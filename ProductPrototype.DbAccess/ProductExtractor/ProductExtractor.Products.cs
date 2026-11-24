using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    /// <summary>
    /// Extract Products From Html
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    private List<Product> ExtractProductsFromHtml(HtmlDocument doc, string productName)
    {
        // Parent product
        var productNode = doc.DocumentNode
                             .SelectSingleNode("//div[@id='product']");

        // Select all tables relevant to products/variants/associated products
        var parent = ExtractParentProduct(productNode, productName);
        lastDbProductDisplayOrder = _context.Products.Where(p => p.ParentGroupedProductId == parent.Id)
                                                 .MaxAsync(e => (int?)e.DisplayOrder).Result ?? 0;

        var xpath = $"//div[@id='pricing']/table[not(contains(concat(' ', normalize-space(@class), ' '), ' hiddenModels '))]";
        var allTables = doc.DocumentNode
                          .SelectNodes(xpath)
                          ?.ToList();

        if (allTables == null || allTables.Count == 0)
            return products;

        var specName = SpecificationTypes.ProductGrouping1.ToString();
        var groupName = SpecificationTypes.ProductGrouping1.ToString();
        string specOption = "";
        int i = 0;
        Product variant = default;



        foreach (var table in allTables)
        {
            if (table.HasClass(headerTblCss))
            {
                specOption = table.SelectSingleNode(".//td").InnerText;
                continue;
            }

            // is product Variant
            var xpathQuery = ".//self::table[contains(@class, 'description')]";
            if (table.SelectSingleNode(xpathQuery) != null)
            {
                int columnCount = table.SelectNodes("./tr[1]/td | ./tr[1]/th")
                                      ?.Count ?? 0;

                if (columnCount >= 6)
                {
                    variant = ExtractProduct(table, i);
                    ExtractProductSpecificationMappings(specName, specOption, variant);
                    //// Assign product picture
                    // GetPropertiesFromHtml(productNode, ref variant, "class/img", "leftL,leftR");
                }
                else if (columnCount <= 4)
                {
                    // skip next element since it’s the associated product
                    var assoc = ExtractProduct(table, i, isAssociated: true);

                    var relatedProduct = relatedProducts
                                               .FirstOrDefault(p => p.ProductId1 == variant.Id && p.ProductId2 == assoc.Id);

                    relatedProduct ??= _context.RelatedProducts
                                               .FirstOrDefault(p => p.ProductId1 == variant.Id && p.ProductId2 == assoc.Id);

                    if (relatedProduct == null)
                    {
                        lastRelatedProductId = (lastRelatedProductId == 0 ? lastDbRelatedProductId : lastRelatedProductId) + 1;
                        //populate RelatedProducts
                        relatedProducts.Add(new RelatedProduct
                        {
                            Id = lastRelatedProductId,
                            ProductId1 = variant.Id,
                            ProductId2 = assoc.Id,
                            DisplayOrder = 1
                        });
                    }

                }
                i++;
            }
        }

        return products;
    }

    // 3️⃣ Serialize to JSON
    private Product ExtractParentProduct(HtmlNode element, string productName)
    {

        Product product = default;
        // Find matching attribute
        var prod = products.FirstOrDefault(a => a.Sku.Equals(ProductSku, StringComparison.OrdinalIgnoreCase));
        prod ??= _context.Products.FirstOrDefault(a => a.Sku.ToLower().Equals(ProductSku.ToLower()));
        if (prod == null)
        {
            lastProductId = (lastProductId == 0 ? lastDbProductId : lastProductId) + 1;
            product = new Product
            {
                Id = lastProductId,
                ParentGroupedProductId = 0,
                ProductTypeId = 10,
                OrderMinimumQuantity = 1,
                OrderMaximumQuantity = 10,
                Published = true,
                Deleted = false,
                Name = productName,
                ShowOnHomepage = true,
                AvailableStartDateTimeUtc = DateTime.UtcNow,
            };

            //Extract product grouping header
            var producttemplateId = GetPageTemplate(element, product);
            product.ProductTemplateId = producttemplateId;

            //get parent Produt Name
            GetPropertiesFromHtml(element, ref product, "h1", "h1");

            //get parent Full description
            GetPropertiesFromHtml(element, ref product, "class", "wrapper");

            //get parent Full description
            GetPropertiesFromHtml(element, ref product, "id", "product");

            product.Sku = ProductSku; // Parent may not have SKU
            product.ParentGroupedProductId = 0; // Top-level
            product.ProductTypeId = 10; // Group Parent
            product.DisplayOrder = 0; // Optional: set if needed
            product.CreatedOnUtc = DateTime.UtcNow;
            if (producttemplateId == 0)
            {
                throw new Exception("Product Template Id is missing");
            }
            ;
            products.Add(product);
            lastParentProduct = product;
        }
        else
        {
            product = prod;
            lastParentProduct = prod;
        }

        var specName = SpecificationTypes.ProductGrouping1.ToString();
        var groupName = SpecificationTypes.ProductGrouping1.ToString();

        var tables = element.SelectNodes($"//div[@id ='pricing']//table[@class ='{headerTblCss}']");
        foreach (var table in tables)
        {
            var attrOption = table.SelectSingleNode(".//td")?.InnerText;
            if (attrOption != null)
            {
                ExtractProductSpecificationMappings(specName, attrOption, product);
            }

        }

        return product;
    }

    private Product ExtractProduct(HtmlNode table, int displayOrder, bool isAssociated = false)
    {

        if (lastParentProduct == null)
        {
            throw new Exception("Parent Product is missing");
        }

        // SKU
        var skuNode = table.SelectSingleNode(".//td[contains(@class,'model')]");
        if (skuNode == null)
        {
            skuNode = table.SelectSingleNode(".//td[contains(@class,'ocModel')]"); ;
        }
        string productSku = string.Empty;
        string productName = string.Empty;
        if (skuNode != null)
        {
            productSku = skuNode.InnerText.Trim();
        }
        else if (isAssociated == true)
        {
            throw new Exception();
        }

        var product = products.FirstOrDefault(p => p.Sku == productSku);
        product ??= _context.Products.FirstOrDefault(p => p.Sku == productSku);

        if (product == null)
        {
            lastProductId = (lastProductId == 0 ? lastDbProductId : lastProductId) + 1;
            lastDbProductDisplayOrder++;

            product = new Product
            {
                ParentGroupedProductId = isAssociated ? 0 : lastParentProduct.Id,
                Id = lastProductId,
                Sku = productSku,
                Name = lastParentProduct.Name + "-" + productSku,
                Published = true,
                Deleted = false,
                ProductTypeId = 5,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                OrderMinimumQuantity = 1,
                OrderMaximumQuantity = 10,
                ShowOnHomepage = true,
                AvailableStartDateTimeUtc = DateTime.UtcNow,
                DisplayOrder = lastDbProductDisplayOrder      // Optional: can set based on element order
            };

            Console.WriteLine($"assoc=>{isAssociated}, {product.ParentGroupedProductId}");

            if (isAssociated)
            {
                ExtractAssociatedProducts(table, ref product);
            }
            else
            {
                // Weight
                var weightNode = table.SelectSingleNode(".//td[contains(@class,'lbs')]");
                if (weightNode != null)
                {
                    var weightText = weightNode.InnerText.Replace("&nbsp;", "")
                                                         .Replace("(", "")
                                                         .Replace(")", "")
                                                         .Replace("lbs", "").Trim();

                    if (decimal.TryParse(weightText, out decimal w))
                        product.Weight = w;
                }

                // Price
                var priceNodes = table.SelectNodes(".//td[contains(@class,'price')]");
                product.Price = 0;
                foreach (var priceNode in priceNodes)
                {
                    if (priceNode != null)
                    {
                        var priceText = priceNode.InnerText.Replace("$", "").Trim();
                        if (decimal.TryParse(priceText, out decimal p))
                        {
                            if (product.Price == 0)
                                product.Price = p;
                            else
                            {
                                product.OldPrice = product.Price;
                                product.Price = p;
                            }
                        }
                    }
                }
            }


            (var rowColumnStyles, string rowClass, string cssName) = ExtractProductColumnStyle(table);

            var productTmpName = $"{rowColumnStyles.Count - 1}TCell.{cssName}.{rowClass}";
            var templateStyle = SerializeToJson(rowColumnStyles).ReplaceLineEndings(""); ;

            var producttemplateId = GetProductTmp(productTmpName, templateStyle, viewPath: "ProductTemplate.Simple");
            if (producttemplateId == 0)
            {
                throw new Exception("Product Template Id is missing");
            }
            ;
            product.ProductTemplateId = producttemplateId;
            Console.WriteLine(product.ParentGroupedProductId);
            products.Add(product);
        }

        var row = table.SelectSingleNode(".//tr");
        var attributValues = ExtractProductAttributeValues(row, product);

        var cells = row.SelectNodes("td");
        string specName = string.Empty;
        var cellColumnIndex = 1;
        if (table.SelectNodes(".//tr/td").Count == 8) cellColumnIndex = 2;
        if (cells[cellColumnIndex] != null)
        {
            specName = cells[cellColumnIndex].InnerText;
            var prodSpecMappings = ExtractProductSpecificationMappings(SpecificationTypes.TableColumn1.ToString(), specName, product);
        }
        //product.ProductTemplateId
        return product;
    }

    private Product ExtractAssociatedProducts(HtmlNode assocTable, ref Product product)
    {
        // ----------------------------
        // ASSOCIATED PRODUCT TABLE (descriptionBorder1)
        // ----------------------------
        var assocRows = assocTable.SelectNodes(".//tr");
        if (assocRows != null)
        {
            foreach (var row in assocRows)
            {
                var assocSku = row.SelectSingleNode(".//td[contains(@class,'ocModel')]")?.InnerText.Trim();
                var assocDetail = row.SelectSingleNode(".//td[contains(@class,'ocDetail')]")?.InnerText.Trim();

                // Extract bold section (<b>Optional Covers</b>)
                var nameNode = row.SelectSingleNode(".//td[contains(@class,'ocDetail')]/b");
                var attrName = nameNode?.InnerText.Trim() ?? "";

                // Extract description (text after the <b>...</b>)
                var descText = assocDetail?.Replace(attrName, "").Trim();
                if (string.IsNullOrEmpty(descText))
                    continue;

                int index = descText.IndexOf('$');

                if (index > -1)
                    descText = descText.Substring(0, index); // remove leading dash

                // Extract price (like "$44/each")
                decimal assocPrice = 0;
                string priceValue = "";
                string priceUnit = "";
                var priceMatch = Regex.Match(assocDetail ?? "", @"\$(\d+(\.\d+)?)(?:\/(\w+))?");
                if (priceMatch.Success)
                {
                    priceValue = priceMatch.Groups[1].Value;
                    priceUnit = priceMatch.Groups[3].Success ? priceMatch.Groups[3].Value : "";
                    assocPrice = decimal.Parse(priceMatch.Groups[1].Value, CultureInfo.InvariantCulture);
                }

                // Extract Yes/No inputs
                var inputs = row.SelectNodes(".//span[@class='rtx']");
                var inputValues = inputs?.Select(x => x.InnerText.Trim()).ToList() ?? new List<string> { "Yes", "No" };

                // Add to combinations list
                product.Sku = assocSku;
                product.Price = assocPrice;
            }
        }
        return product;
    }
}
