using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using ProductPrototype.DbAccess.Data.Models;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using File = System.IO.File;


namespace ProductPrototype.DbAccess;

public class ProductHeader
{
    public int ColumnIndex { get; set; }        // column order
    public string CssClass { get; set; }        // td/th class
    public string Style { get; set; }           // inline style or inherited
    public string ParentTableStyle { get; set; } // e.g. border color, width, etc.
}


public enum SpecificationTypes
{
    TableColumn1,
    ProductGrouping1,
}

public class Dimension()
{
    public string  SpecificationOptionKey { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
}


public class HtmlProductExtractor
{
    private List<ProductHeader> headers = new();

    private List<Product> products = new();

    private List<ProductAttribute> productAttributes = new();
    private List<PredefinedProductAttributeValue> predefinedProductAttributeValues = new();
    private List<ProductAttributeValue> productAttributeValues = new();

    private List<SpecificationAttributeGroup> specificationAttributeGroups = new();
    private List<SpecificationAttribute> specificationAttributes = new();

    private List<ProductProductAttributeMapping> productProductAttributeMappings = new();
    private List<ProductSpecificationAttributeMapping> productSpecificationAttributeMappings = new();
    private List<SpecificationAttributeOption> specificationAttributeOptions = new();
    private List<ProductAttributeCombination> productAttributeCombinations = new();
    private List<RelatedProduct> relatedProducts = new();

    private List<Picture> pictures = new();
    private List<ProductPictureMapping> productPictureMappings = new();
    private List<Dimension> dimensions = new();


    private int lastDbProductAttributeId = 0;
    private int predefinedDbProductAttributeValueId = 0;
    private int lastDbSpecificationAttributeGroupId = 0;
    private int lastDbSpecificationAttributeId = 0;
    private int lastDbSpecificationAttributeOptionId = 0;
    private int lastDbProductId = 0;

    private int lastDbProductProductAttributeMappingId = 0;
    private int lastDbProductAttributeValueId = 0;

    private int lastDbProductSpecificationAttributeMappingId = 0;
    private int lastDbRelatedProductId = 0;


    private int lastProductAttributeId = 0;
    private int predefinedProductAttributeValueId = 0;
    private int lastSpecificationAttributeGroupId = 0;
    private int lastSpecificationAttributeId = 0;
    private int lastSpecificationAttributeOptionId = 0;
    private int lastProductId = 0;
    private int lastDbProductDisplayOrder = 0;
    private int lastDbPictureId = 0;
    private int lastDbPictureMappingId = 0;


    private int lastProductProductAttributeMappingId = 0;
    private int lastProductAttributeValueId = 0;

    private int lastProductSpecificationAttributeMappingId = 0;
    private int lastRelatedProductId = 0;

    // pictures
    private int lastPictureId = 0;
    private int lastPictureMappingId = 0;
    private Product lastParentProduct = default;

    private readonly RepositoryContext _context;
    public HtmlProductExtractor(RepositoryContext context)
    {
        _context = context;
    }

    public async Task ExtractAllProductsFromHtmlAsync(int parentid)
    {
        string htmlContent = string.Empty;

        //var url = "https://www.belson.com/Grosfillex-Nautical-Sling-Chaise-Lounge";
        //var url = "https://www.belson.com/UpLift-Bike-Rack";
        //var url = "https://www.belson.com/32-Gallon-Steel-Trash-Receptacle-with-Diamond-Pattern";
        //var url = "https://www.belson.com/Classic-Style-Flat-Park-Bench-with-Diamond-Pattern-Steel";
        var url = "https://www.belson.com/Classic-Style-Flat-Park-Bench-with-Diamond-Pattern-Steel";
        //var url = "https://www.belson.com/Coated-Steel-MOD-Benches-without-Back-Slatted-Steel";
        htmlContent = GetHtmlFromUrlAsync(url).Result;

        //string filePath = Path.Combine(Environment.CurrentDirectory, @"wwwroot/html/products/Park-Bench.html"); //Park-Bench,Picnic-element,Concrete-Bench"
        //htmlContent = ReadHtmlFromFile(filePath);

        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var allProducts = new List<Product>();

        string json = string.Empty;

        try
        {

            lastDbProductAttributeId = await _context.ProductAttributes
                                                     .MaxAsync(e => (int?)e.Id) ?? 0;

            predefinedDbProductAttributeValueId = await _context.PredefinedProductAttributeValues
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;

            lastDbSpecificationAttributeGroupId = await _context.SpecificationAttributeGroups
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;
            lastDbSpecificationAttributeId = await _context.SpecificationAttributes
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;
            lastDbSpecificationAttributeOptionId = await _context.SpecificationAttributeOptions
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;
            lastDbSpecificationAttributeOptionId = await _context.SpecificationAttributeOptions
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;
            lastDbProductId = await _context.Products
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;


            lastDbProductProductAttributeMappingId = await _context.ProductProductAttributeMappings
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;

            lastDbProductAttributeValueId = await _context.ProductAttributeValues
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;
            lastDbProductSpecificationAttributeMappingId = await _context.ProductSpecificationAttributeMappings
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;
            lastDbRelatedProductId = await _context.RelatedProducts
                                                                .MaxAsync(e => (int?)e.Id) ?? 0;

            lastDbPictureId = await _context.Pictures
                                                    .MaxAsync(e => (int?)e.Id) ?? 0;

            lastDbPictureMappingId = await _context.ProductPictureMappings
                                                    .MaxAsync(e => (int?)e.Id) ?? 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //var headers = ExtractDynamicHeaders(htmlContent);
        ExtractAttributesAndSpecifications(doc);
        var variants = ExtractProductsFromHtml(doc);
        allProducts.AddRange(variants);

        ////Export to JSON Format 
        //json = ExportToJson(htmlContent, doc, allProducts);



        Console.WriteLine("✅ .Database.Begin Transaction Async");

        // 1. Begin the Transaction
        // IDbContextTransaction is IDisposable and should be used in a 'using' block
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {

            try
            {
                // === TRANSACTIONAL OPERATIONS START ===

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.ProductAttribute ON");
                foreach (var entity in productAttributes)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.ProductAttributes.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.ProductAttributes.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.ProductAttributes.Update(entity);
                        //var entry = _context.Entry(entity);
                        //entry.State = EntityState.Modified;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.ProductAttribute OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.PredefinedProductAttributeValue ON");
                foreach (var entity in predefinedProductAttributeValues)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.PredefinedProductAttributeValues.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.PredefinedProductAttributeValues.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.ProductAttributes.Update(entity);
                        //var entry = _context.Entry(entity);
                        //entry.State = EntityState.Modified;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.PredefinedProductAttributeValue OFF");



                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.SpecificationAttributeGroup ON");
                foreach (var entity in specificationAttributeGroups)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.SpecificationAttributeGroups.Any(item => item.Id == entity.Id))// or attr.Id == Guid.Empty for Guid keys
                    {

                        // Case 1: NEW Entity
                        _context.SpecificationAttributeGroups.Add(entity);

                    }
                    else
                    {
                        // Case 2: UPDATED Entity

                        // _context.SpecificationAttributeGroups.Update(entity);
                        // _context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }

                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.SpecificationAttributeGroup OFF");

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.SpecificationAttribute ON");
                foreach (var entity in specificationAttributes)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.SpecificationAttributes.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {

                        // Case 1: NEW Entity
                        _context.SpecificationAttributes.Add(entity);


                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.SpecificationAttributes.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.SpecificationAttribute OFF");

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.SpecificationAttributeOption ON");
                foreach (var entity in specificationAttributeOptions)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.SpecificationAttributeOptions.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {

                        // Case 1: NEW Entity
                        _context.SpecificationAttributeOptions.Add(entity);

                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.SpecificationAttributeOptions.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.SpecificationAttributeOption OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Picture ON");
                foreach (var entity in pictures)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.Pictures.Any(item => item.Id == entity.Id))  // or attr.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.Pictures.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.Products.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Picture OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product ON");
                foreach (var entity in allProducts)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.Products.Any(item => item.Sku == entity.Sku))  // or attr.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.Products.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.Products.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product OFF");

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_ProductAttribute_Mapping ON");
                foreach (var entity in productProductAttributeMappings)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.ProductProductAttributeMappings.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.ProductProductAttributeMappings.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.ProductAttributeValues.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_ProductAttribute_Mapping OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_Picture_Mapping ON");
                foreach (var entity in productPictureMappings)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.ProductPictureMappings.Any(item => item.ProductId == entity.ProductId && item.PictureId == entity.PictureId)) // or attr.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.ProductPictureMappings.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.ProductAttributeValues.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_Picture_Mapping OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.ProductAttributeValue ON");
                foreach (var entity in productAttributeValues)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.ProductAttributeValues.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {

                        // Case 1: NEW Entity
                        _context.ProductAttributeValues.Add(entity);

                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.ProductAttributeValues.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.ProductAttributeValue OFF");

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_SpecificationAttribute_Mapping ON");
                foreach (var entity in productSpecificationAttributeMappings)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.ProductSpecificationAttributeMappings.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {

                        // Case 1: NEW Entity
                        _context.ProductSpecificationAttributeMappings.Add(entity);

                    }
                    else
                    {
                        // // Case 2: UPDATED Entity
                        // _context.ProductSpecificationAttributeMappings.Update(entity);
                        //// _context.Entry(entity).Property<string>(entity.ProductId).IsModified = true;
                    }

                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_SpecificationAttribute_Mapping OFF");

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.RelatedProduct ON");
                foreach (var entity in relatedProducts)
                {
                    // 1. Check if the attr is NEW or UPDATED based on its ID
                    if (!_context.RelatedProducts.Any(item => item.Id == entity.Id)) // or attr.Id == Guid.Empty for Guid keys
                    {

                        // Case 1: NEW Entity
                        _context.RelatedProducts.Add(entity);

                    }
                    else
                    {
                        // // Case 2: UPDATED Entity
                        // _context.RelatedProducts.Update(entity);
                        //// _context.Entry(entity).Property<int>(entity.ProductId1).IsModified = true;
                    }

                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.RelatedProduct OFF");


                // Call SaveChanges again for the second change. 
                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // 3. Rollback the Transaction
                // If ANY SaveChanges or other operation failed, all changes are undone.
                await transaction.RollbackAsync();

                // Log the exception or re-throw it for higher-level handling
                Console.WriteLine($"Transaction failed: {ex.Message}");
                throw;
            }
        } // The transaction is disposed here



        Console.WriteLine("✅ Extraction complete!");

        string ExportToJson(string htmlContent, HtmlDocument doc, List<Product> allProducts)
        {

            string json;

            //Console.WriteLine("private List<ProductHeader> = ");
            Console.WriteLine("const productHeadersocmodel = ");
            json = SerializeToJson(headers);
            Console.WriteLine(json + ";");


            Console.WriteLine();
            //Console.WriteLine("private List<SpecificationAttribute> = ");
            Console.WriteLine("const specificationAttributeGroups = ");
            json = SerializeToJson(specificationAttributeGroups);
            // Console.WriteLine("Specifications");
            Console.WriteLine(json + ";");

            Console.WriteLine();
            //Console.WriteLine("private List<SpecificationAttribute> = ");
            Console.WriteLine("const specificationAttributes = ");
            json = SerializeToJson(specificationAttributes);
            // Console.WriteLine("Specifications");
            Console.WriteLine(json + ";");

            json = SerializeToJson(specificationAttributeOptions);
            Console.WriteLine();
            //Console.WriteLine("private List<SpecificationAttributeOption> = ");
            Console.WriteLine("const specificationAttributeOptions = ");
            Console.WriteLine(json + ";");



            Console.WriteLine();
            //Console.WriteLine("private List<Product> = ");
            Console.WriteLine("const products = ");
            json = SerializeToJson(allProducts);
            Console.WriteLine(json + ";");

            //Console.WriteLine();
            ////Console.WriteLine("private List<Product> = ");
            //Console.WriteLine("const productAttributeCombinations = ");
            //json = SerializeToJson(productAttributeCombinations);
            //Console.WriteLine(json + ";");



            Console.WriteLine();
            //Console.WriteLine("private List<Product> = ");
            Console.WriteLine("const relatedProducts = ");
            json = SerializeToJson(relatedProducts);
            Console.WriteLine(json + ";");




            Console.WriteLine();
            //Console.WriteLine("private List<ProductAttribute> = ");
            Console.WriteLine("const productAttributes = ");
            json = SerializeToJson(productAttributes);
            Console.WriteLine(json + ";");


            Console.WriteLine();
            //Console.WriteLine("private List<Product_ProductAttribute_Mapping> = ");
            Console.WriteLine("const product_ProductAttribute_Mappings = ");
            json = SerializeToJson(productProductAttributeMappings);
            Console.WriteLine(json + ";");

            Console.WriteLine();
            // Console.WriteLine("private List<ProductAttributeValue> = ");
            Console.WriteLine("const productAttributeValues = ");
            json = SerializeToJson(productAttributeValues);
            Console.WriteLine(json + ";");

            Console.WriteLine();
            //Console.WriteLine("private List<Product_SpecificationAttribute_Mapping> = ");
            Console.WriteLine("const product_SpecificationAttribute_Mappings = ");
            json = SerializeToJson(productSpecificationAttributeMappings);
            Console.WriteLine(json + ";");
            return json;
        }

    }

    private string ReadHtmlFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        return File.ReadAllText(filePath);
    }

    private async Task<string> GetHtmlFromUrlAsync(string url)
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
    private List<Product> ExtractProductsFromHtml(HtmlDocument doc)
    {
        // Parent product

        if (lastProductId == 0) lastProductId = lastDbProductId;

        var productNode = doc.DocumentNode
                             .SelectSingleNode("//div[@id='product']");

        // Select all tables relevant to products/variants/associated products
        var parent = ExtractParentProduct(productNode);
        lastDbProductDisplayOrder = _context.Products.Where(p => p.ParentGroupedProductId == parent.Id)
                                                     .MaxAsync(e => (int?)e.DisplayOrder).Result ?? 0;


        // Select all tables relevant to products/variants/associated products
        var allTables = doc.DocumentNode
                           .SelectNodes("//table[contains(@class,'ptExpanded2') or contains(@class,'description') or contains(@class,'descriptionBorder1') or contains(@class,'ptConcrete') or contains(@class,'ptPool') or contains(@class,'ptSale') or contains(@class,'ptBenches') ]")
                          ?.ToList();

        if (allTables == null || allTables.Count == 0)
            return products;

        var specName = SpecificationTypes.ProductGrouping1.ToString();
        var groupName = SpecificationTypes.ProductGrouping1.ToString();
        string attrOption = "";

        int i = 0;
        Product variant = default;
        foreach (var table in allTables)
        {
            if (table.HasClass("ptExpanded2"))
            {
                attrOption = table.SelectSingleNode(".//span").InnerText;
            }

            // Variant product

            var xpathQuery = ".//self::table[contains(@class, 'description')]";
            if (table.SelectSingleNode(xpathQuery) != null)
            {

                if (table.SelectSingleNode(".//td[contains(@class,'ocModel')]") == null)
                {
                    variant = ExtractProduct(table, i);
                    ExtractProductSpecificationMappings(specName, attrOption, variant);
                   // Assign product picture
                   GetPropertiesFromHtml(productNode, ref variant, "class/img", "leftR");
                }
                else
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



    // Helper to parse weight or price string to decimal
    private decimal ParseDecimal(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0m;

        // Remove non-numeric chars, e.g., $, lbs, spaces
        string cleaned = Regex.Replace(text, @"[^\d\.]", "");
        return decimal.TryParse(cleaned, out decimal result) ? result : 0m;
    }

    private List<ProductHeader> ExtractDynamicHeaders(string htmlContent)
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
    private Product ExtractParentProduct(HtmlNode element)
    {
        lastProductId = (lastProductId == 0) ? lastDbProductId + 1 : lastProductId + 1;
        Product product = new Product { Id = lastProductId, ParentGroupedProductId = 10, Published = true };
        //get parent Produt Name
        GetPropertiesFromHtml(element, ref product, "h1", "h1");

        //get parent Full description
        GetPropertiesFromHtml(element, ref product, "class", "wrapper");

        //get parent image
        GetPropertiesFromHtml(element, ref product, "class/img", "leftR");

        //get products dimensions
        GetPropertiesFromHtml(element, ref product, "div/class", "dim");

        //get parent short description
        GetPropertiesFromHtml(element, ref product, "class", "rightR");

        //get parent Full description
        GetPropertiesFromHtml(element, ref product, "id", "product");
     

        // Find matching attribute
        var prod = products.FirstOrDefault(a => a.Sku.Equals("Park-Bench-01" + lastProductId, StringComparison.OrdinalIgnoreCase));
        prod ??= _context.Products.FirstOrDefault(a => a.Sku.ToLower().Equals("Park-Bench-01".ToLower() + lastProductId));
        if (prod == null)
        {
            product.Sku = "Park-Bench-" + product.Id; // Parent may not have SKU
            product.ParentGroupedProductId = 0; // Top-level
            product.ProductTypeId = 10; // Group Parent
            product.DisplayOrder = 0; // Optional: set if needed
            product.CreatedOnUtc = DateTime.UtcNow;
            products.Add(product);
        }


        var specName = SpecificationTypes.ProductGrouping1.ToString();
        var groupName = SpecificationTypes.ProductGrouping1.ToString();

        var tables = element.SelectNodes("//table[contains(@class,'ptExpanded2')]");
        foreach (var table in tables)
        {
            var attrOption = table.SelectSingleNode(".//span").InnerText;
            ExtractProductSpecificationMappings(specName, attrOption, product);
        }
        lastParentProduct = product;
        return product;
    }

    private void GetPropertiesFromHtml(HtmlNode element, ref Product product, string selectBy, string selectVal = "")
    {
        // Name and Length from h2 > span inside the element
        string nameNode;
        HtmlNode lengthNode = default;
        HtmlNode elementNode = default;
        string xpathQuery = string.Empty;


        switch (selectBy)
        {
            case "h1":
                elementNode = element.SelectSingleNode(".//" + selectVal);
                product.Name = elementNode.InnerText.Trim();
                elementNode?.InnerHtml = "{product-header-title}";
                break;

            case "h2":
                lengthNode = element.SelectSingleNode(".//span");
                if (lengthNode != null)
                {
                    // Parse length as decimal if needed, else keep as string
                    string lengthText = lengthNode.InnerText.Trim().Replace("'", "").Replace("’", "");
                    if (decimal.TryParse(lengthText, out decimal len))
                        product.Length = len;
                }
                break;

            case "class/img":
                elementNode = element.SelectSingleNode("//div[@class ='" + selectVal + "']");
                var imgNode = elementNode.SelectSingleNode(".//img");
                if (imgNode != null)
                {
                    var imgDocTag = new HtmlDocument();
                    imgDocTag.LoadHtml(imgNode.OuterHtml);
                    var productImage = imgDocTag.DocumentNode.Descendants("img").FirstOrDefault();
                    string imgSource = productImage.GetAttributeValue("src", string.Empty);

                    // Parse length as decimal if needed, else keep as string
                    string imgText = imgNode.InnerText.Trim().Replace("'", "").Replace("’", "");

                    lastPictureId = (lastPictureId == 0) ? lastDbPictureId + 1 : lastPictureId + 1;

                    var picture = new Picture
                    {
                        Id = lastPictureId,
                        MimeType = "image/jpeg",
                        SeoFilename = imgSource,
                        IsNew = false,
                    };
                    pictures.Add(picture);

                    lastPictureMappingId = (lastPictureMappingId == 0) ? lastDbPictureMappingId + 1 : lastPictureMappingId + 1;
                    var ProductPictureMapping = new ProductPictureMapping
                    {
                        Id = lastPictureMappingId,
                        ProductId = product.Id,
                        PictureId = picture.Id,
                        DisplayOrder = 0,
                    };
                    productPictureMappings.Add(ProductPictureMapping);

                }
                break;

            case "id":
                elementNode = element.SelectSingleNode("//div[@id ='" + selectVal + "']");
                if (selectVal == "product")
                {
                    product.FullDescription = elementNode?.InnerHtml;
                }

                break;

            case "class":
                elementNode = element.SelectSingleNode("//div[@class ='" + selectVal + "']");
                if (selectVal == "rightR")
                {
                    xpathQuery = "//*[contains(concat(' ', normalize-space(@class), ' '), ' " + selectVal + " ')]/p";
                    var shortDecsNode = element.SelectSingleNode(xpathQuery);
                    product.ShortDescription = shortDecsNode?.InnerText;
                    shortDecsNode?.InnerHtml = "{short-description}";
                }
                else if (selectVal == "wrapper")
                {
                    var colorOptions = element.SelectNodes("//div[@class ='" + selectVal + "']");
                    colorOptions[colorOptions.Count - 1].InnerHtml = "<div id=\"colorContainer\"><div id=\"spheresContainer\"></div></div>";


                }
                break;

            //case "class/img":
            //    xpathQuery = "//*[contains(concat(' ', normalize-space(@class), ' '), ' " + selectVal + " ')]/img";
            //    elementNode = element.SelectSingleNode(xpathQuery);
            //    break;

            case "div/class":
                xpathQuery = "//div[contains(concat(' ', normalize-space(@class), ' '), '" + selectVal + "')]";
                var elementNodes = element.SelectNodes(xpathQuery);
                foreach (var el in elementNodes)
                {
                    if (el.HasClass("dimT")) continue;

                    ExtractWholePartsSimplified(el.InnerText);
               

                    el.Remove();
                }
                break;

            default:
                break;
        }
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
        else
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
                ParentGroupedProductId = isAssociated? 0: lastParentProduct.Id,
                Id = lastProductId,
                Sku = productSku,
                Name = lastParentProduct.Name + "-" + productSku,
                Published = true,
                ProductTypeId = 5,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                DisplayOrder = lastDbProductDisplayOrder      // Optional: can set based on element order
            };

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
            //var dims = dimensions.FirstOrDefault(dim => dim.SpecificationOptionKey == "key");
            //if (dims != null)
            //{
            //    product.Length = dims.Length;
            //    product.Width = dims.Width;
            //    product.Height = dims.Height;
            //}

            products.Add(product);
        }

        if (isAssociated)
        {
           return  ExtractAssociatedProducts(table, product);
           
        }
        var row = table.SelectSingleNode(".//tr");
        if (product.Sku == "942S-V8")
        {
            Console.WriteLine();
        }

        var attributValues = ExtractProductAttributeValues(row, product);

        var cells = row.SelectNodes("td");
        string specName = string.Empty;
        if (cells[1] != null)
        {
            specName = cells[1].InnerText;
            var prodSpecMappings = ExtractProductSpecificationMappings(SpecificationTypes.TableColumn1.ToString(), specName, product);
        }
        return product;
    }

    private void ExtractAttributesAndSpecifications(HtmlDocument doc)
    {

        string specName = string.Empty;
        string groupName = string.Empty;

        specName = SpecificationTypes.ProductGrouping1.ToString();
        groupName = SpecificationTypes.ProductGrouping1.ToString();

        CreateGroupingSpecifications(groupName, specName, doc);

        foreach (var row in doc.DocumentNode.SelectNodes("//table[contains(@class, 'description')]//tr") ?? Enumerable.Empty<HtmlNode>())
        {

            if (row.SelectNodes("./td")?.Count <= 4)
            {
                continue;
            }
            var cells = row.SelectNodes("td");
            if (cells == null) continue;

            // Detect Specification 1 (specificationAttributes)
            int specIndex = 1;
            if (row.SelectSingleNode(".//td[contains(@class,'alpha')]") != null)
            {
                specIndex = 2;
            }
            string specText = HtmlEntity.DeEntitize(cells[specIndex].InnerText.Trim());

            specName = SpecificationTypes.TableColumn1.ToString();
            groupName = SpecificationTypes.TableColumn1.ToString();

            CreateSpecifications(specText, groupName, specName);


            // Detect <select> dropdowns (Product Attributes)
            var selects = row.SelectNodes(".//select");
            if (selects == null) continue;

            foreach (var select in selects)
            {
                string selectedLabel = select.SelectSingleNode("option[@selected]")?.InnerText.Trim() ?? "";

                // Unify Seat Color / Frame Color → "Color"
                string attributeName = selectedLabel.Contains("Choose", StringComparison.OrdinalIgnoreCase) && selectedLabel.Contains("Color", StringComparison.OrdinalIgnoreCase) ? selectedLabel.Replace("Choose ", "").Trim() : selectedLabel;
                if (string.IsNullOrWhiteSpace(attributeName)) continue;

                // Check if the attr exists
                var attr = productAttributes
                                  .FirstOrDefault(a => a.Name == attributeName);

                attr ??= _context.ProductAttributes
                                   .FirstOrDefault(a => a.Name == attributeName);



                if (attr == null)
                {
                    lastProductAttributeId = (lastProductAttributeId == 0 ? lastDbProductAttributeId : lastProductAttributeId) + 1;
                    // Create a new attr
                    attr = new ProductAttribute { Id = lastProductAttributeId };
                    attr.Name = attributeName;
                    productAttributes.Add(attr);
                }


            }
        }


    }

    private void CreateGroupingSpecifications(string groupName, string specName, HtmlDocument doc)
    {
        var allTables = doc.DocumentNode
            .SelectNodes("//table[contains(@class,'ptExpanded2')]")
            .ToList();

        if (allTables == null)
        {
            return;
        }

        foreach (var table in allTables)
        {
            if (table.HasClass("ptExpanded2"))
            {
                var specOption = table.SelectSingleNode(".//span");

                if (specOption != null)
                {
                    var specGroup = specificationAttributeGroups
                                                            .FirstOrDefault(grp => grp.Name == groupName);

                    specGroup ??= _context.SpecificationAttributeGroups
                                                            .FirstOrDefault(grp => grp.Name == groupName);
                    if (specGroup == null)
                    {
                        lastSpecificationAttributeGroupId = (lastSpecificationAttributeGroupId == 0 ? lastDbSpecificationAttributeGroupId : lastSpecificationAttributeGroupId) + 1;
                        specGroup = new SpecificationAttributeGroup { Id = lastSpecificationAttributeGroupId, Name = groupName, DisplayOrder = 0 };
                        specificationAttributeGroups.Add(specGroup);
                    }

                    var spec = specificationAttributes
                              .FirstOrDefault(s => s.Name.Equals(specName, StringComparison.OrdinalIgnoreCase));

                    spec ??= _context.SpecificationAttributes.FirstOrDefault(s => s.Name == specName);



                    if (spec == null)
                    {
                        lastSpecificationAttributeId = (lastSpecificationAttributeId == 0 ? lastDbSpecificationAttributeId : lastSpecificationAttributeId) + 1;
                        spec = new SpecificationAttribute { Id = lastSpecificationAttributeId, Name = specName, SpecificationAttributeGroupId = specGroup.Id };

                        specificationAttributes.Add(spec);
                    }


                    var specificationAttributeOption = specificationAttributeOptions
                                                               .FirstOrDefault(attr => attr.Name == specOption.InnerText);

                    specificationAttributeOption ??= _context.SpecificationAttributeOptions
                                                               .FirstOrDefault(attr => attr.Name == specOption.InnerText);

                    if (specificationAttributeOption == null)
                    {
                        int displayOrder = specificationAttributeOptions
                                          .Where(attr => attr.SpecificationAttributeId == lastSpecificationAttributeId)
                                          .OrderByDescending(itm => itm.DisplayOrder)
                                          .FirstOrDefault()?
                                          .DisplayOrder ?? 0;

                        //pull from the db
                        if (displayOrder == 0)
                        {
                            displayOrder = _context.SpecificationAttributeOptions
                                                      .Where(attr => attr.SpecificationAttributeId == lastSpecificationAttributeId)
                                                      .MaxAsync(e => (int?)e.DisplayOrder).Result ?? 0;

                        }

                        lastSpecificationAttributeOptionId = (lastSpecificationAttributeOptionId == 0 ? lastDbSpecificationAttributeOptionId : lastSpecificationAttributeOptionId) + 1;

                        specificationAttributeOptions.Add(new SpecificationAttributeOption
                        {
                            Id = lastSpecificationAttributeOptionId,
                            Name = specOption.InnerText,
                            SpecificationAttributeId = lastSpecificationAttributeId,
                            DisplayOrder = displayOrder + 1
                        });
                    }
                }
            }

        }
    }
    private void CreateSpecifications(string specText, string groupName, string specName)
    {
        var specGroup = specificationAttributeGroups
                              .FirstOrDefault(s => s.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));

        specGroup ??= _context.SpecificationAttributeGroups.FirstOrDefault(s => s.Name == groupName);


        if (specGroup == null)
        {
            lastSpecificationAttributeGroupId = (lastSpecificationAttributeGroupId == 0 ? lastDbSpecificationAttributeGroupId : lastSpecificationAttributeGroupId) + 1;
            specGroup = new SpecificationAttributeGroup { Id = lastSpecificationAttributeGroupId, Name = groupName, DisplayOrder = 0 };
            specificationAttributeGroups.Add(specGroup);
        }


        var spec = specificationAttributes
                              .FirstOrDefault(s => s.Name.Equals(specName, StringComparison.OrdinalIgnoreCase));
        spec ??= _context.SpecificationAttributes.FirstOrDefault(s => s.Name == specName);


        if (spec == null)
        {
            lastSpecificationAttributeId = (lastSpecificationAttributeId == 0 ? lastDbSpecificationAttributeId : lastSpecificationAttributeId) + 1;
            spec = new SpecificationAttribute { Id = lastSpecificationAttributeId, Name = specName, SpecificationAttributeGroupId = specGroup.Id };
            specificationAttributes.Add(spec);
        }

        var specOption = specificationAttributeOptions.FirstOrDefault(s => s.Name.Equals(specText, StringComparison.OrdinalIgnoreCase));
        specOption ??= _context.SpecificationAttributeOptions.FirstOrDefault(s => s.Name.ToLower() == specText.ToLower());



        if (specOption == null)
        {
            lastSpecificationAttributeOptionId = (lastSpecificationAttributeOptionId == 0 ? lastDbSpecificationAttributeOptionId : lastSpecificationAttributeOptionId) + 1;
            var displayOrder = _context.SpecificationAttributeOptions.MaxAsync(e => (int?)e.DisplayOrder).Result ?? 0;
            displayOrder++;
            specificationAttributeOptions.Add(new SpecificationAttributeOption
            {
                Id = lastSpecificationAttributeOptionId,
                SpecificationAttributeId = spec.Id,
                Name = specText,
                DisplayOrder = displayOrder,
            });
        }
    }

    private List<ProductAttributeValue> ExtractProductAttributeValues(HtmlNode row, Product product)
    {
        if (row == null) return productAttributeValues;

        var selects = row.SelectNodes(".//select");
        if (selects == null) return productAttributeValues;

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
            var attribute = productAttributes
                              .FirstOrDefault(a => a.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));

            attribute ??= _context.ProductAttributes
                                .FirstOrDefault(a => a.Name.ToLower().Equals(attributeName.ToLower()));
            // If no match found, continue
            if (attribute == null)
                continue;

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
            }
        }

        return productAttributeValues;
    }

    private List<ProductSpecificationAttributeMapping> ExtractProductSpecificationMappings(string specName, string attrOption, Product product)
    {
        //Console.WriteLine("products:" + products.Count);
        // HtmlEntity.DeEntitize(cells[1].InnerText.Trim());
        var spec = specificationAttributes
                                .FirstOrDefault(s => s.Name == specName);

        spec ??= _context.SpecificationAttributes
                                .FirstOrDefault(s => s.Name == specName);

        if (spec == null)
        {
            throw new Exception();
            // return productSpecificationAttributeMappings;
        }
        var option = specificationAttributeOptions
                                .FirstOrDefault(o => o.Name.Equals(attrOption, StringComparison.OrdinalIgnoreCase)
                                                  && o.SpecificationAttributeId == spec.Id);

        option ??= _context.SpecificationAttributeOptions
                           .FirstOrDefault(o => o.Name.ToLower() == attrOption.ToLower()
                                             && o.SpecificationAttributeId == spec.Id);

        var lastSpecificationAttributeMappingDisplayOrder = 0;
        if (option == null)
        {
            return productSpecificationAttributeMappings;
        }
        else
        {
            ProductSpecificationAttributeMapping specMapp = default;

            specMapp = productSpecificationAttributeMappings.OrderByDescending(spec => spec.DisplayOrder)
                                    .FirstOrDefault(opt => opt.ProductId == product.Id && opt.Id == option.Id);

            specMapp ??= _context.ProductSpecificationAttributeMappings.OrderByDescending(spec => spec.DisplayOrder)
                                .FirstOrDefault(opt => opt.ProductId == product.Id && opt.Id == option.Id);


            if (specMapp == null)
            {
                lastProductSpecificationAttributeMappingId = (lastProductSpecificationAttributeMappingId == 0 ? lastDbProductSpecificationAttributeMappingId : lastProductSpecificationAttributeMappingId) + 1;
                lastSpecificationAttributeMappingDisplayOrder++;

                productSpecificationAttributeMappings.Add(new ProductSpecificationAttributeMapping
                {
                    Id = lastProductSpecificationAttributeMappingId,
                    ProductId = product.Id,
                    SpecificationAttributeOptionId = option.Id,
                    DisplayOrder = lastSpecificationAttributeMappingDisplayOrder
                });
              
            }
        }

        return productSpecificationAttributeMappings;
    }

    private Product ExtractAssociatedProducts(HtmlNode assocTable, Product product)
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


    // Helper to parse weight like "(94 lbs)"
    private decimal ParseWeight(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0m;
        var match = Regex.Match(text, @"(\d+(\.\d+)?)");
        return match.Success ? decimal.Parse(match.Value) : 0m;
    }

    private string SerializeToJson(object data)
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



    public void ExtractWholePartsSimplified(string dimensionString)
    {
        // 1. Split the string by the separator " x "
        // We use StringSplitOptions.None for simplicity, but it's important to specify the string separator.
        string[] dimension = dimensionString.Split(new string[] { " - " }, StringSplitOptions.None);

        if (dimension.Length == 2)
            dimension = dimension[1].Split(new string[] { " x " }, StringSplitOptions.None);

        if (dimension.Length != 3)
        {
            Console.WriteLine("Error: String structure is unexpected. Could not find 3 dimensions.");
        }

        // --- 2. Extract and Clean Each Part ---

        // Part 1: "72"L"
        string lengthStr = dimension[0].Replace("&quot;L", "");
        decimal length = decimal.Parse(lengthStr);

        // Part 2: "22"W"
        string widthStr = dimension[1].Replace("&quot;W", "");
        decimal width = decimal.Parse(widthStr);

        // Part 3: "35-3/16"H" 
        string heightValueStr = dimension[2].Replace("&quot;H", "");

        // --- 3. Extract the Whole Part of Height ---

        // The height string is now "35-3/16". We split it at the first hyphen.
        string wholeHeightStr = heightValueStr.Split('-')[0];
        decimal height = decimal.Parse(wholeHeightStr);
        //var keys = dimensionString.Split("-");
        //var key = (keys?.Count() > 0 ? keys[0].ToString() : "");

        //dimensions.Add(new Dimension
        //{
        //    SpecificationOptionKey = key,
        //    Length = length,
        //    Width = width,
        //    Height = height
        //});
    
    }

    public async Task UpdateDbAsync<TEntity>(string sqlTableName, List<TEntity> entitiesToProcess, bool exists = false) where TEntity : class
    {

        if (!exists)
        {
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{sqlTableName} ON");
        }

        foreach (var entity in entitiesToProcess)
        {
            _context.Update(entity); // Works for both detached Add and Update states
        }

        // Execute all changes against the database
        await _context.SaveChangesAsync();

        if (!exists)
        {
            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{sqlTableName} OFF");
        }


    }
}
