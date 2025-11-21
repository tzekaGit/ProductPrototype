using HtmlAgilityPack;
using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Dotnet.Workflow.WebApp;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sku { get; set; }
    public int ProductTypeId { get; set; }
    public int? ParentGroupedProductId { get; set; }
    public string ShortDescription { get; set; }
    public string FullDescription { get; set; }
    public bool RequireOtherProducts { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOnUtc { get; set; } = DateTime.UtcNow;
    public List<ProductAttribute> Attributes { get; set; } = new();
}

// Specification (DB only, no navigation properties)
public class SpecificationAttribute
{
    public int Id { get; set; } // PK
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
}

public class SpecificationAttributeOption
{
    public int Id { get; set; } // PK
    public int SpecificationAttributeId { get; set; } // FK
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
}

// Product Attributes
public class ProductAttribute
{
    public int Id { get; set; } // PK
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ProductAttributeValue
{
    public int Id { get; set; } // PK
    public string Name { get; set; } = string.Empty;
    public int ProductAttributeMappingId { get; set; } // FK
    public int AttributeValueTypeId { get; set; } = 0; // default simple text
    public string Context { get; set; } = string.Empty; // optional
    public int DisplayOrder { get; set; } = 0;
}

// Product → Attribute Mapping
public class Product_ProductAttribute_Mapping
{
    public int Id { get; set; } // PK
    public int ProductId { get; set; }
    public int ProductAttributeId { get; set; }
}

// Product → Specification Mapping
public class Product_SpecificationAttribute_Mapping
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SpecificationAttributeOptionId { get; set; }
    public int DisplayOrder { get; set; } = 0;
}

public class ProductHeader
{
    public int ColumnIndex { get; set; }        // column order
    public string CssClass { get; set; }        // td/th class
    public string Style { get; set; }           // inline style or inherited
    public string ParentTableStyle { get; set; } // e.g. border color, width, etc.
}

public class RelatedProduct
{
    public int Id { get; set; }                  // Primary key (optional if you use auto-increment in DB)
    public int ProductId1 { get; set; }          // The parent or main product
    public int ProductId2 { get; set; }          // The associated product
    public int DisplayOrder { get; set; }        // Order in which the association appears
    public bool IsRequired { get; set; }         // True if the associated product is mandatory
    public DateTime CreatedOnUtc { get; set; }   // When this association was created
    public DateTime UpdatedOnUtc { get; set; }   // When it was last updated
}

public class ProductAttributeCombination
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string AttributesXml { get; set; }
    public string Sku { get; set; }
    public string ManufacturerPartNumber { get; set; }
    public string Gtin { get; set; }
    public decimal? OverriddenPrice { get; set; }
    public int StockQuantity { get; set; }
    public bool AllowOutOfStockOrders { get; set; }
    public decimal? WeightAdjustment { get; set; }
    public decimal? PriceAdjustment { get; set; }
    public int? PictureId { get; set; }
    public bool IsActive { get; set; }
}


public static class HtmlProductExtractor
{
    public static List<ProductHeader> headers = new();
    public static List<Product> products = new();
    public static List<Product_ProductAttribute_Mapping> attributeMappings = new();
    public static List<ProductAttribute> attrs = new();
    public static List<ProductAttributeValue> attrValues = new();
    public static List<SpecificationAttribute> specs = new();
    public static List<Product_SpecificationAttribute_Mapping> specMappings = new();
    public static List<SpecificationAttributeOption> specOptions = new();
    public static List<ProductAttributeCombination> productAttributeCombinations = new();
    public static List<RelatedProduct> relatedProducts = new();


    public static void ExtractAllProductsFromHtml()
    {
        string htmlContent = string.Empty;

        //var url = "https://www.belson.com/Grosfillex-Nautical-Sling-Chaise-Lounge";
        //var url = "https://www.belson.com/UpLift-Bike-Rack";
        var url = "https://www.belson.com/32-Gallon-Steel-Trash-Receptacle-with-Diamond-Pattern";
        //var url = "https://www.belson.com/Coated-Steel-MOD-Benches-without-Back-Slatted-Steel";
        htmlContent = GetHtmlFromUrlAsync(url).Result;

        //string filePath = Path.Combine(Environment.CurrentDirectory, @"Concrete-Bench.html"); //Park-Bench,Picnic-table,Concrete-Bench"
        //htmlContent = ReadHtmlFromFile(filePath);

        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var allProducts = new List<Product>();

        string json = string.Empty;

        //json = SerializeToJson(allProducts);
        //Console.WriteLine(json);


        var headers = ExtractDynamicHeaders(htmlContent);
        //Console.WriteLine("public List<ProductHeader> = ");
        Console.WriteLine("const productHeadersocmodel = ");
        json = SerializeToJson(headers);
        Console.WriteLine(json + ";");

        ExtractAttributesAndSpecifications(doc);

        Console.WriteLine();
        //Console.WriteLine("public List<SpecificationAttribute> = ");
        Console.WriteLine("const specificationAttributes = ");
        json = SerializeToJson(specs);
        // Console.WriteLine("Specifications");
        Console.WriteLine(json + ";");

        json = SerializeToJson(specOptions);
        Console.WriteLine();
        //Console.WriteLine("public List<SpecificationAttributeOption> = ");
        Console.WriteLine("const specificationAttributeOptions = ");
        Console.WriteLine(json + ";");

        var variants = ExtractProductsFromHtml(doc);
        allProducts.AddRange(variants);

        Console.WriteLine();
        //Console.WriteLine("public List<Product> = ");
        Console.WriteLine("const products = ");
        json = SerializeToJson(allProducts);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<Product> = ");
        Console.WriteLine("const productAttributeCombinations = ");
        json = SerializeToJson(productAttributeCombinations);
        Console.WriteLine(json + ";");



        Console.WriteLine();
        //Console.WriteLine("public List<Product> = ");
        Console.WriteLine("const relatedProducts = ");
        json = SerializeToJson(relatedProducts);
        Console.WriteLine(json + ";");




        Console.WriteLine();
        //Console.WriteLine("public List<ProductAttribute> = ");
        Console.WriteLine("const productAttributes = ");
        json = SerializeToJson(attrs);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        // Console.WriteLine("public List<ProductAttributeValue> = ");
        Console.WriteLine("const productAttributeValues = ");
        json = SerializeToJson(attrValues);
        Console.WriteLine(json + ";");


        Console.WriteLine();
        //Console.WriteLine("public List<Product_ProductAttribute_Mapping> = ");
        Console.WriteLine("const product_ProductAttribute_Mappings = ");
        json = SerializeToJson(attributeMappings);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<Product_SpecificationAttribute_Mapping> = ");
        Console.WriteLine("const product_SpecificationAttribute_Mappings = ");
        json = SerializeToJson(specMappings);
        Console.WriteLine(json + ";");



        Console.WriteLine("✅ Extraction complete!");
    }

    public static string ReadHtmlFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        return File.ReadAllText(filePath);
    }

    public static async Task<string> GetHtmlFromUrlAsync(string url)
    {
        // 1. Create an instance of HttpClient
        using (var client = new HttpClient())
        {
            try
            {
                // 2. Make an asynchronous GET request to the URL
                // GetStringAsync handles the entire process: making the request, 
                // waiting for the response, and reading the response content as a string.
                string html = await client.GetStringAsync(url);

                // 3. Return the HTML content
                return html;
            }
            catch (HttpRequestException e)
            {
                // Handle potential errors, such as a bad URL or network issues
                Console.WriteLine($"\nRequest Exception Caught!");
                Console.WriteLine($"Message: {e.Message}");
                return null;
            }
        }
    }

    // 🧩 2. Extract child/variant products
    // Updated method signature: pass only parentId (and optionally parentGroupingValue for grouping)
    public static List<Product> ExtractProductsFromHtml(HtmlDocument doc)
    {
        // Select all tables relevant to products/variants/associated products
        var allTables = doc.DocumentNode
            .SelectNodes("//table[contains(@class,'ptExpanded2') or contains(@class,'description') or contains(@class,'descriptionBorder1') or contains(@class,'ptConcrete') or contains(@class,'ptPool') or contains(@class,'ptSale') or contains(@class,'ptBenches') ]")
            ?.ToList();

        if (allTables == null || allTables.Count == 0)
            return products;

        int lastParentId = 0;

        for (int i = 0; i < allTables.Count; i++)
        {
            var table = allTables[i];

            // Parent product
            if (table.HasClass("ptExpanded2") || table.HasClass("ptConcrete") || table.HasClass("ptPool") || table.HasClass("ptSale") || table.HasClass("ptBenches"))
            {
                var parent = ExtractParentProduct(table);
                lastParentId = parent.Id;
            }
            // Variant product
            else if (table.HasClass("description") || table.HasClass("descriptionBorder2"))
            {
                //var productId = products.Count + 1;
                var variant = ExtractProduct(table);
                //variant.Id = productId;
                //products.Add(variant);
                bool hasAssoc = false;
                // Check if next table is an associated product (descriptionBorder1 after description)
                if (i + 1 < allTables.Count && allTables[i + 1].HasClass("descriptionBorder1"))
                {
                    var assocTbl = allTables[i + 1];
                    if (assocTbl.SelectNodes("//td[contains(@class,'ocModel')]") != null)
                    {
                        hasAssoc = true;
                    }
                }
                if (hasAssoc)
                {
                    i++; // skip next table since it’s the associated product
                    var assoc = ExtractProduct(allTables[i]);
                    //assoc.Id = products.Count + 1;
                    //products.Add(assoc);

                    //populate RelatedProducts
                    relatedProducts.Add(new RelatedProduct
                    {
                        Id = relatedProducts.Count + 1,
                        ProductId1 = variant.Id,
                        ProductId2 = assoc.Id,
                        DisplayOrder = 1
                    });


                    ExtractProductsAndCombinations(allTables[i], assoc.Id);
                }
            }
            // Sometimes a standalone product table (descriptionBorder1) not related to a variant
            else if (table.HasClass("descriptionBorder1"))
            {

                var standalone = ExtractProduct(table);
                //standalone.Id = products.Count + 1;
                //products.Add(standalone);
            }
        }

        return products;
    }



    // Helper to parse weight or price string to decimal
    private static decimal ParseDecimal(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0m;

        // Remove non-numeric chars, e.g., $, lbs, spaces
        string cleaned = Regex.Replace(text, @"[^\d\.]", "");
        return decimal.TryParse(cleaned, out decimal result) ? result : 0m;
    }

    public static List<ProductHeader> ExtractDynamicHeaders(string htmlContent)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(htmlContent);


        var table = doc.DocumentNode.SelectSingleNode("//table[contains(@class,'description')]");
        if (table == null) return headers;

        var row = table.SelectSingleNode(".//tr");
        if (row == null) return headers;

        var tds = row.Elements("td").ToList();

        for (int i = 0; i < tds.Count; i++)
        {
            var td = tds[i];

            // Collect only <th>-related metadata (visuals & structure)
            string css = td.GetAttributeValue("class", "");
            string style = td.GetAttributeValue("style", "");

            // If no style, fallback to first child element with style
            if (string.IsNullOrWhiteSpace(style))
            {
                var childWithStyle = td.Descendants().FirstOrDefault(x => x.Attributes["style"] != null);
                if (childWithStyle != null)
                    style = childWithStyle.GetAttributeValue("style", "");
            }

            headers.Add(new ProductHeader
            {
                ColumnIndex = i,
                CssClass = css,
                Style = style,
                ParentTableStyle = table.GetAttributeValue("style", "")
            });
        }

        return headers;
    }

    // 3️⃣ Serialize to JSON
    public static Product ExtractParentProduct(HtmlNode table)
    {
        var product = new Product();

        // Name and Length from h2 > span inside the table
        var nameNode = table.SelectSingleNode(".//h2");
        if (nameNode != null)
        {
            product.Name = nameNode.InnerText.Trim();
            var lengthNode = nameNode.SelectSingleNode(".//span");
            if (lengthNode != null)
            {
                // Parse length as decimal if needed, else keep as string
                string lengthText = lengthNode.InnerText.Trim().Replace("'", "").Replace("’", "");
                if (decimal.TryParse(lengthText, out decimal len))
                    product.Length = len;
            }
        }

        product.Sku = ""; // Parent may not have SKU
        product.ParentGroupedProductId = 0; // Top-level
        product.DisplayOrder = 0; // Optional: set if needed
        product.CreatedOnUtc = DateTime.UtcNow;
        product.Id = products.Count + 1;

        products.Add(product);
        return product;
    }

    public static Product ExtractProduct(HtmlNode table)
    {
        var parentGroupedProductId = products.Where(p => p.ParentGroupedProductId == 0)
                                             .OrderByDescending(p => p.Id)
                                             .FirstOrDefault()
                                             .Id;
        var product = new Product
        {
            ParentGroupedProductId = parentGroupedProductId,
            Id = products.Count + 1,
            CreatedOnUtc = DateTime.UtcNow,
            DisplayOrder = 0 // Optional: can set based on table order
        };
        products.Add(product);
        //Console.WriteLine(product.Id);
        // SKU
        var skuNode = table.SelectSingleNode(".//td[contains(@class,'model')]");

        if (skuNode == null)
        {
            skuNode = table.SelectSingleNode(".//td[contains(@class,'ocModel')]"); ;
        }

        if (skuNode != null)
            product.Sku = skuNode.InnerText.Trim();

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
        var priceNode = table.SelectSingleNode(".//td[contains(@class,'price')]");
        if (priceNode != null)
        {
            var priceText = priceNode.InnerText.Replace("$", "").Trim();
            if (decimal.TryParse(priceText, out decimal p))
                product.Price = p;
        }

        var row = table.SelectSingleNode(".//tr");
        var attributValues = ExtractProductAttributeValues(row);
        ExtractProductSpecificationMappings(row);
        return product;
    }

    public static void ExtractAttributesAndSpecifications(HtmlDocument doc)
    {
        foreach (var row in doc.DocumentNode.SelectNodes("//table[contains(@class, 'description')]//tr") ?? Enumerable.Empty<HtmlNode>())
        {
            var cells = row.SelectNodes("td");
            if (cells == null) continue;

            // Detect Specification 1 (specification)
            int specIndex = 1;
            if (row.SelectSingleNode(".//td[contains(@class,'alpha')]") != null)
            {
                specIndex = 2;
            }
            string specText = HtmlEntity.DeEntitize(cells[specIndex].InnerText.Trim());
            string specName = specText;

            var spec = specs.FirstOrDefault(s => s.Name == "Specification 1");
            if (spec == null)
            {
                spec = new SpecificationAttribute { Id = specs.Count + 1, Name = "Specification 1" };
                specs.Add(spec);
            }

            if (!specOptions.Any(o => o.Name == specText))
            {
                specOptions.Add(new SpecificationAttributeOption
                {
                    Id = specOptions.Count + 1,
                    SpecificationAttributeId = spec.Id,
                    Name = specText,
                    DisplayOrder = specOptions.Count
                });
            }


            // Detect <select> dropdowns (Product Attributes)
            var selects = row.SelectNodes(".//select");
            if (selects == null) continue;

            foreach (var select in selects)
            {
                string selectedLabel = select.SelectSingleNode("option[@selected]")?.InnerText.Trim() ?? "";

                // Unify Seat Color / Frame Color → "Color"
                string attributeName = selectedLabel.Contains("Choose", StringComparison.OrdinalIgnoreCase) && selectedLabel.Contains("Color", StringComparison.OrdinalIgnoreCase) ? selectedLabel.Replace("Choose ", "").Trim() : selectedLabel;
                if (string.IsNullOrWhiteSpace(attributeName)) continue;

                var attr = attrs.FirstOrDefault(a => a.Name == attributeName);
                if (attr == null)
                {
                    attr = new ProductAttribute { Id = attrs.Count + 1, Name = attributeName, Description = "" };
                    attrs.Add(attr);
                }
            }
        }
    }
    public static List<ProductAttributeValue> ExtractProductAttributeValues(HtmlNode row)
    {
        if (row == null) return attrValues;

        var selects = row.SelectNodes(".//select");
        if (selects == null) return attrValues;

        foreach (var select in selects)
        {
            // Get the first option (the "Choose..." placeholder) to determine if this is a color attribute
            var firstOption = select.SelectSingleNode("option[@selected]");
            if (firstOption == null) continue;

            string firstOptionValue = firstOption.GetAttributeValue("value", "").Trim();
            string firstOptionText = HtmlEntity.DeEntitize(firstOption.InnerText.Trim());

            // Check if this select is a color attribute by looking for "Choose" + "Color" pattern
            bool isColorAttribute = firstOptionValue.StartsWith("Choose", StringComparison.OrdinalIgnoreCase) &&
                                   (firstOptionValue.IndexOf("Color", StringComparison.OrdinalIgnoreCase) >= 0);

            // If it's not a color attribute, skip this entire select and move to next column
            if (!isColorAttribute)
                continue;

            // Extract attribute name from the first option
            string attributeName = firstOptionValue
                .Replace("Choose", "", StringComparison.OrdinalIgnoreCase)
                .Trim();

            // Find matching attribute
            var attribute = attrs
                .FirstOrDefault(a => a.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));

            if (attribute == null)
                continue;

            // Create or reuse product-attribute mapping
            var mapping = attributeMappings.FirstOrDefault(m =>
                                            m.ProductId == products.Count && m.ProductAttributeId == attribute.Id);

            if (mapping == null)
            {
                var mappingId = attributeMappings.Count + 1;
                mapping = new Product_ProductAttribute_Mapping
                {
                    Id = mappingId,
                    ProductId = products.Count,
                    ProductAttributeId = attribute.Id
                };
                attributeMappings.Add(mapping);
            }

            // Extract valid <option> nodes (excluding disabled and the first "Choose" option)
            var optionNodes = select.SelectNodes("option[not(@selected) and not(@disabled)]");
            if (optionNodes == null)
                continue;

            int displayOrder = 0;
            foreach (var opt in optionNodes)
            {
                // Extract value attribute
                string valueAttr = opt.GetAttributeValue("value", "").Trim();
                string displayText = HtmlEntity.DeEntitize(opt.InnerText.Trim());

                // Skip if empty
                if (string.IsNullOrWhiteSpace(valueAttr))
                    continue;


                // Add per-attribute unique value
                bool alreadyExists = attrValues.Any(v =>
                    v.Name.Equals(displayText, StringComparison.OrdinalIgnoreCase) &&
                    v.ProductAttributeMappingId == mapping.Id);

                if (!alreadyExists)
                {
                    attrValues.Add(new ProductAttributeValue
                    {
                        Id = attrValues.Count + 1,
                        ProductAttributeMappingId = mapping.Id,
                        Name = displayText,
                        AttributeValueTypeId = 0,
                        DisplayOrder = displayOrder++,
                        Context = valueAttr // Store original value for reference
                    });
                }
            }
        }

        return attrValues;
    }

    public static List<Product_SpecificationAttribute_Mapping> ExtractProductSpecificationMappings(HtmlNode productRow)
    {
        //Console.WriteLine("products:" + products.Count);
        var cells = productRow.SelectNodes("td");
        if (cells == null) return specMappings;


        if (cells[2] != null)
        {
            string specText = HtmlEntity.DeEntitize(cells[1].InnerText.Trim());
            var spec = specs.FirstOrDefault(s => s.Name == "Specification 1");
            if (spec == null) return specMappings;

            var option = specOptions.FirstOrDefault(o => o.Name.Equals(specText, StringComparison.OrdinalIgnoreCase)
                                                         && o.SpecificationAttributeId == spec.Id);
            if (option == null) return specMappings;

            specMappings.Add(new Product_SpecificationAttribute_Mapping
            {
                Id = specMappings.Count + 1,
                ProductId = products.Count,
                SpecificationAttributeOptionId = option.Id,
                DisplayOrder = 0
            });
        }

        return specMappings;
    }
    public static void ExtractProductsAndCombinations(HtmlNode assocTable, int productId)
    {
        // ----------------------------
        // ASSOCIATED PRODUCT TABLE (descriptionBorder1)
        // ----------------------------

        if (assocTable != null)
        {
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

                    // Build custom AttributesXml
                    var attributesXml = $@"
                                        <Attributes>
                                          <Name>{System.Security.SecurityElement.Escape(attrName)}</Name>
                                          <Description>{System.Security.SecurityElement.Escape(descText)}</Description>
                                          <Price>
                                            <Value>{System.Security.SecurityElement.Escape(priceValue)}</Value>
                                            <PriceUnit>{System.Security.SecurityElement.Escape(priceUnit)}</PriceUnit>
                                          </Price>
                                          <Inputs>
                                        {string.Join(Environment.NewLine, inputValues.Select(v => $"    <Input>{System.Security.SecurityElement.Escape(v)}</Input>"))}
                                          </Inputs>
                                        </Attributes>".Trim();

                    // Add to combinations list
                    var combo = new ProductAttributeCombination
                    {
                        Id = productAttributeCombinations.Count + 1,
                        ProductId = productId,
                        Sku = assocSku,
                        OverriddenPrice = assocPrice,
                        AttributesXml = attributesXml,
                        IsActive = true,
                        AllowOutOfStockOrders = true,
                        StockQuantity = 0,

                    };
                    products.Find(p => p.Id == productId).Price = assocPrice;
                    productAttributeCombinations.Add(combo);
                }
            }
        }
    }


    // Helper to parse weight like "(94 lbs)"
    private static decimal ParseWeight(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0m;
        var match = Regex.Match(text, @"(\d+(\.\d+)?)");
        return match.Success ? decimal.Parse(match.Value) : 0m;
    }

    public static string SerializeToJson(object data)
    {
        if (data is IEnumerable<object> collection)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            var items = collection.Select(item => JsonSerializer.Serialize(item, options));

            // ✅ This gives you commas with line breaks and proper indentation
            return "[\n  " + string.Join(",\n  ", items) + "\n]";
        }

        // For single objects, use standard formatting
        return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    }



}

