using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;
using ProductPrototype.DbAccess.Interfaces;
using ProductPrototype.DbAccess.Services;
using System.Text.Json;

namespace ProductPrototype.DbAccess;

public class GetDataFromDb : IGetDataFromDb
{
    readonly ProductService _productService;
    public GetDataFromDb(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductDetailDto>> GetProductsAsync(int parentId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");
                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                var productDetails = await _productService.GetProductDetailsAsync(parentId);
                foreach (var prod in productDetails)
                {
                    if (prod.ParentGroupedProductId != 0) { 
                        prod.ProductAttributes = _productService.GetProductAttributesAsync(prod.Id).Result;
                        prod.ProductSpecificationAttributeOptions = _productService.GetProductSpecificationAsync(prod.ProductId).Result;
                    }
                   
                }
                return productDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }

    public static async Task<List<ProductProductAttributeMapping>> GetProductAttributeMappingsAsync(int productId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<ProductProductAttributeMapping> productAttributeMappings = await context.ProductProductAttributeMappings
                    .Where(p => p.ProductId == productId)
                    .ToListAsync();

                return productAttributeMappings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
    public static async Task<List<ProductAttribute>> GetProductAttributesAsync(int ProductAttributeId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<ProductAttribute> productAttributes = await context.ProductAttributes
                    .Where(p => p.Id == ProductAttributeId)
                    .ToListAsync();

                return productAttributes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public static async Task<List<ProductAttributeValue>> GetProductAttributeValuesAsync(int ProductAttributeMappingId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<ProductAttributeValue> productAttributes = await context.ProductAttributeValues
                    .Where(p => p.ProductAttributeMappingId == ProductAttributeMappingId)
                    .ToListAsync();

                return productAttributes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public static async Task<List<SpecificationAttribute>> GetSpecificationAttributesAsync(int specificationAttributeId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<SpecificationAttribute> specificationAttributes = await context.SpecificationAttributes
                    .Where(p => p.Id == specificationAttributeId)
                    .ToListAsync();

                return specificationAttributes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public static async Task<List<ProductSpecificationAttributeMapping>> GetProductSpecificationAttributeMappingsAsync(int productId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<ProductSpecificationAttributeMapping> productSpecificationAttributeMappings = await context.ProductSpecificationAttributeMappings
                    .Where(p => p.ProductId == productId)
                    .ToListAsync();

                return productSpecificationAttributeMappings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public static async Task<List<SpecificationAttributeOption>> GetSpecificationAttributeOptionsAsync(int specificationAttributeOptionId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<SpecificationAttributeOption> specificationAttributeOptions = await context.SpecificationAttributeOptions
                    .Where(p => p.Id == specificationAttributeOptionId)
                    .ToListAsync();

                return specificationAttributeOptions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
    public static async Task<List<RelatedProduct>> GetRelatedProductsAsync(int productId)
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");

                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                List<RelatedProduct> relatedProducts = await context.RelatedProducts
                    .Where(p => p.ProductId1 == productId)
                    .ToListAsync();

                return relatedProducts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
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