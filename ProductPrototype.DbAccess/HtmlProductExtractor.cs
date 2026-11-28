using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.Models;
using ProductPrototype.DbAccess.Interfaces;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    public HtmlProductExtractor(RepositoryContext context, IJsonConfigStore jsonConfigStore)
    {
        _context = context;
        _jsonConfigStore = jsonConfigStore;
    }

    public async Task ExtractAllProductsFromHtmlAsync(string productName = "")
    {
        string htmlContent = string.Empty;
        if (!string.IsNullOrEmpty(productName))
        {
            url = "https://www.belson.com/" + productName;
        }
        else
        {
            //url = "https://www.belson.com/Classic-Style-Park-Bench-with-Diamond-Pattern-Steel"; // good seven column template

            //url = "https://www.belson.com/Classic-Style-Flat-Park-Bench-with-Diamond-Pattern-Steel"; //good standard template 
            /// 
            /// Both  attributes in label format
            ///
            //url = "https://www.belson.com/Powder-Coated-Round-Tube-Wave-Bike-Racks"; // good with old price seven columnst  template
            //url = "https://www.belson.com/Grosfillex-Nautical-Sling-Chaise-Lounge"; // good- six column tml
            // url = "https://www.belson.com/E-Series-Steel-Backless-Bench"; // good 7 column template/ green color
            //url = "https://www.belson.com/UpLift-Bike-Rack"; //good red color
            //url = "https://www.belson.com/Buckaroo-Playground-Structure"; // six column template / green color


            url = "https://www.belson.com/32-Gallon-Steel-Trash-Receptacle-with-Diamond-Pattern"; //complex with dropdown & labels
                                                                                                  //url = "https://www.belson.com/Fiberglass-Umbrellas-Bistro"; // related products fails

            //url = "https://www.belson.com/Recycled-Plastic-Malibu-Park-Bench-Portable-Mount"; //too complex
            // url = "https://www.belson.com/E-Series-Steel-Picnic-Table-Rectangular"; //good, the first columns should be TableColumn1
            // url = "https://www.belson.com/E-Series-Steel-Backless-Bench";  //good

            // url = "https://www.belson.com/Coated-Steel-MOD-Benches-without-Back-Slatted-Steel";
        }

        if (url == string.Empty)
            throw new Exception();

        ProductSku = productName;

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
            lastDbProductTemplateId = await _context.ProductTemplates.MaxAsync(e => (int?)e.Id) ?? 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        ExtractSpecifications(doc);
        var variants = ExtractProductsFromHtml(doc, productName);
        allProducts.AddRange(variants);

        ////Export to JSON Format 
        //json = ExportToJson(htmlContent, node, allProducts);
        List<UrlRecord> UrlRecords = new();
        int lastUrlRecordId = await _context.UrlRecords.MaxAsync(e => (int?)e.Id) ?? 0;

        foreach (var prod in allProducts)
        {
            if (_context.UrlRecords.Any(url => url.EntityId == prod.Id))
                continue;
            {

                UrlRecords.Add(new UrlRecord
                {
                    Id = ++lastUrlRecordId,
                    EntityId = prod.Id,
                    EntityName = "Product",
                    Slug = prod.Sku == productName ? productName.ToLower() : $"{productName}-{prod.Sku}".ToLower(),
                    IsActive = true,
                    LanguageId = 0,
                });
            }
        }

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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.ProductAttributes.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.ProductAttributes.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED CT
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.PredefinedProductAttributeValues.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.SpecificationAttributeGroups.Any(item => item.Id == entity.Id))// or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.SpecificationAttributes.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.SpecificationAttributeOptions.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.Pictures.Any(item => item.Id == entity.Id))  // or attributeName.Id == Guid.Empty for Guid keys
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



                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.ProductTemplate ON");
                foreach (var entity in productTemplates)
                {
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.ProductTemplates.Any(item => item.Id == entity.Id))  // or attributeName.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.ProductTemplates.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.Products.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.ProductTemplate OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product ON");
                foreach (var entity in allProducts)
                {
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.Products.Any(item => item.Sku == entity.Sku))  // or attributeName.Id == Guid.Empty for Guid keys
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

                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.UrlRecord ON");
                foreach (var entity in UrlRecords)
                {
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.UrlRecords.Any(item => item.Id == entity.Id))  // or attributeName.Id == Guid.Empty for Guid keys
                    {
                        // Case 1: NEW Entity
                        _context.UrlRecords.Add(entity);
                    }
                    else
                    {
                        //// Case 2: UPDATED Entity
                        //_context.Products.Update(entity);
                        //_context.Entry(entity).Property<string>(entity.Name).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.UrlRecord OFF");


                await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.Product_ProductAttribute_Mapping ON");
                foreach (var entity in productProductAttributeMappings)
                {
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.ProductProductAttributeMappings.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.ProductPictureMappings.Any(item => item.ProductId == entity.ProductId && item.PictureId == entity.PictureId)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.ProductAttributeValues.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.ProductSpecificationAttributeMappings.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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
                    // 1. Check if the attributeName is NEW or UPDATED based on its ID
                    if (!_context.RelatedProducts.Any(item => item.Id == entity.Id)) // or attributeName.Id == Guid.Empty for Guid keys
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

            //Console.WriteLine("private List<ProdRowTmp> = ");
            Console.WriteLine("const productHeadersocmodel = ");
            json = SerializeToJson(prodRowTmps);
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
}
