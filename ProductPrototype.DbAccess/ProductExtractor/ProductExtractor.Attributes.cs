using HtmlAgilityPack;
using ProductPrototype.DbAccess.Data.Models;
using ProductPrototype.DbAccess.ProductExtractor.Models;
using ProductPrototype.DbAccess.Services;
using System.Text.RegularExpressions;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{

    private void CreateAttributeFromText(HtmlNode row, Product product)
    {
        var Paths = _jsonConfigStore.DeserializeSection<List<XPath>>("Paths");
        var PriceOrQTY = Paths.FirstOrDefault(path => path.PathName == "PriceOrQTY")?.PathValue;

        var allColumnNumber = row.SelectNodes(PriceOrQTY)?.Count ?? 0;
        int cellAttrIndex = 0;


        if (row.SelectSingleNode(".//td[1]")?.InnerText == product.Sku)
        {
            cellAttrIndex = 3;
        }
        else if (row.SelectSingleNode(".//td[2]")?.InnerText == product.Sku)
        {
            cellAttrIndex = 4;
        }

        int maxAttrColumns = (allColumnNumber == 4 ? 1 : 2);

        int attributeIndex = 0;
        for (int i = cellAttrIndex; i < cellAttrIndex + maxAttrColumns; i++)
        {

            var attrSelectNode = row.SelectSingleNode($".//td[{i}]//select");
            if (attrSelectNode == null)
            {
                string attrValue = row.SelectSingleNode($".//td[{i}]").InnerText;
                attrValue = Regex.Replace(attrValue, @"\s+", " ").Trim();

                string punctuationPattern = @"[()\[\]\{\},.:;""\'\-\/\\]";
                string xpathQuery = $".//table//tr/td[{cellAttrIndex}]";

                // 2. Use LINQ and a Lambda expression to select and transform the data
                List<string> columnData = row.ParentNode.ParentNode.SelectNodes(xpathQuery)?
                    // The Where clause ensures we only process nodes that have InnerText
                    .Where(n => n != null && !string.IsNullOrWhiteSpace(n.InnerText))
                    // The Select clause applies the cleaning logic to each node's InnerText
                    .Select(cell =>
                    {
                        string cleanedText = Regex.Replace(cell.InnerText, @"\s+", " ").Trim(); // Remove spaces
                        return Regex.Replace(cleanedText, punctuationPattern, " ").Trim(); // Remove punctuation
                    })
                    .Distinct()
                    .ToList()
                    ?? new List<string>(); // Use the Null-Conditional Operator (?.) for safety

                attributeIndex++;
                var attributeName = GeProducttAttribute.GetAttributeName(columnData) + attributeIndex;
                CreateHtmlSelecAtttribute(attributeName);
                HtmlNode newAtrSelectNode = CreateNewAttributeNode(attrValue);
                ProcessAttributeOptions(attributeName, product, newAtrSelectNode);
            }
        }
    }

    private ProductAttribute CreateHtmlSelecAtttribute(string attributeName)
    {
        // Check if the attributeName exists
        var attr = productAttributes
                            .FirstOrDefault(a => a.Name == attributeName);

        attr ??= _context.ProductAttributes
                            .FirstOrDefault(a => a.Name == attributeName);



        if (attr == null)
        {
            lastProductAttributeId = (lastProductAttributeId == 0 ? lastDbProductAttributeId : lastProductAttributeId) + 1;
            // Create a new attributeName
            attr = new ProductAttribute
            {
                Id = lastProductAttributeId,
                Name = attributeName,
                Description = attributeName
            };
            productAttributes.Add(attr);
        }
        return attr;
    }

    private List<ProductAttributeValue> ExtractProductAttributeValues(HtmlNode row, Product product)
    {
        if (row == null) return productAttributeValues;
        var selects = row.SelectNodes(".//select[not(contains(@name,'QTY'))]");

        if (selects != null)
        {
            foreach (var select in selects)
            {

                // Get the first option (the "Choose..." placeholder) to determine if this is a color attribute
                var firstOption = select.SelectSingleNode("option[@selected]");
                if (firstOption == null) continue;

                string firstOptionValue = firstOption.GetAttributeValue("value", "").Trim();
                string firstOptionText = HtmlEntity.DeEntitize(firstOption.InnerText.Trim());

                // Check if this select is a color attribute by looking for "Choose" + "AttributeOption" pattern
                bool isColorAttribute = ((firstOptionValue.StartsWith("Choose", StringComparison.OrdinalIgnoreCase) ||
                                         firstOptionValue.Contains("Frame Color", StringComparison.OrdinalIgnoreCase) ||
                                         firstOptionValue.Contains("Seat Color", StringComparison.OrdinalIgnoreCase)
                                        )
                                        && firstOptionValue.IndexOf("Color", StringComparison.OrdinalIgnoreCase) >= 0);

                // If it's not a color attribute, skip this entire select and move to next column
                if (!isColorAttribute)
                    continue;

                // Extract attribute name from the first option
                string attributeName = firstOptionValue
                                            .Replace("Choose", "", StringComparison.OrdinalIgnoreCase)
                                            .Trim();


                ProcessAttributeOptions(attributeName, product, select);
            }
        }

        if (selects == null || selects.Count == 1)
        {
            CreateAttributeFromText(row, product);
        }

        return productAttributeValues;
    }

    private static HtmlNode CreateNewAttributeNode(string attrValue)
    {
        var attrDoc = new HtmlDocument();
        var attrSelectNode = attrDoc.CreateElement("select");
        var attOptionNode = attrDoc.CreateElement("option");

        attOptionNode.SetAttributeValue("value", attrValue);
        attOptionNode.InnerHtml = attrValue;
        attrSelectNode.AppendChild(attOptionNode);
        return attrSelectNode;
    }

    private void ProcessAttributeOptions(string attributeName, Product product, HtmlNode select)
    {


        // Find matching attribute
        var attribute = productAttributes
                          .FirstOrDefault(a => a.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));

        attribute ??= _context.ProductAttributes
                            .FirstOrDefault(a => a.Name.ToLower().Equals(attributeName.ToLower()));
        // If no match found, continue
        if (attribute == null)
            attribute = CreateHtmlSelecAtttribute(attributeName);

        // Create or reuse product-attribute mapping
        var mapping = productProductAttributeMappings.FirstOrDefault(m =>
                                                    m.ProductId == product.Id && m.ProductAttributeId == attribute.Id);

        mapping = _context.ProductProductAttributeMappings.FirstOrDefault(m =>
                                                         m.ProductId == product.Id && m.ProductAttributeId == attribute.Id);

        if (mapping == null)
        {
            lastProductProductAttributeMappingId = (lastProductProductAttributeMappingId == 0 ? lastDbProductProductAttributeMappingId : lastProductProductAttributeMappingId) + 1;
            mapping = new ProductProductAttributeMapping
            {
                Id = lastProductProductAttributeMappingId,
                ProductId = product.Id,
                ProductAttributeId = attribute.Id,
                AttributeControlTypeId = 1
            };
            productProductAttributeMappings.Add(mapping);
        }

        // Extract valid <option> nodes (excluding disabled and the first "Choose" option)
        var optionNodes = select.SelectNodes("option[not(@selected) and not(@disabled)]");
        int displayOrder = 0;

        if (optionNodes != null && optionNodes.Count > 0)
        {
            foreach (var opt in optionNodes)
            {
                // Extract value attribute
                string valueAttr = opt.GetAttributeValue("value", "").Trim();
                string displayText = HtmlEntity.DeEntitize(opt.InnerText.Trim());

                // Skip if empty
                if (string.IsNullOrWhiteSpace(valueAttr))
                    continue;

                displayOrder = CreateUpdateAttributeOptions(attribute, mapping, displayOrder, displayText);
            }
        }
    }

    private int CreateUpdateAttributeOptions(ProductAttribute attribute, ProductProductAttributeMapping mapping, int displayOrder, string displayText)
    {

        // Add per-attribute unique value
        bool predAlreadyExists = predefinedProductAttributeValues.Any(v =>
                                             v.Name.Equals(displayText, StringComparison.OrdinalIgnoreCase));

        if (predAlreadyExists == false)
            predAlreadyExists = _context.PredefinedProductAttributeValues.Any(v =>
                                            v.Name == displayText);

        var predDisplayOrder = 0;
        if (!predAlreadyExists)
        {
            predefinedProductAttributeValueId = (predefinedProductAttributeValueId == 0 ? predefinedDbProductAttributeValueId : predefinedProductAttributeValueId) + 1;
            predDisplayOrder++;
            if (displayText == "Frame Color")
            {
                throw new Exception();
            }
            predefinedProductAttributeValues.Add(new PredefinedProductAttributeValue
            {
                Id = predefinedProductAttributeValueId,
                Name = displayText,
                IsPreSelected = false,
                ProductAttributeId = attribute.Id,
                DisplayOrder = predDisplayOrder,
            });
        }



        // Add per-attribute unique value
        bool alreadyExists = productAttributeValues.Any(v =>
                                      v.Name.Equals(displayText, StringComparison.OrdinalIgnoreCase) &&
                                      v.ProductAttributeMappingId == mapping.Id);
        if (alreadyExists == false)
            alreadyExists = _context.ProductAttributeValues.Any(v =>
                                       v.Name == displayText &&
                                       v.ProductAttributeMappingId == mapping.Id);
        if (!alreadyExists)
        {
            lastProductAttributeValueId = (lastProductAttributeValueId == 0 ? lastDbProductAttributeValueId : lastProductAttributeValueId) + 1;

            displayOrder++;
            productAttributeValues.Add(new ProductAttributeValue
            {
                Id = lastProductAttributeValueId,
                ProductAttributeMappingId = mapping.Id,
                Name = displayText,
                AttributeValueTypeId = 0,
                DisplayOrder = displayOrder,
            });
        }

        return displayOrder;
    }
}
