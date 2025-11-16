using ProductPrototype.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPrototype.DbAccess;

namespace ProductPrototype.Pages;

public class IndexModel : PageModel
{
    public static List<Product> products = new();
    public static List<ProductProductAttributeMapping> attributeMappings = new();
    public static List<ProductAttribute> attrs = new();
    public static List<ProductAttributeValue> attrValues = new();
    public static List<SpecificationAttribute> specs = new();
    public static List<ProductSpecificationAttributeMapping> specMappings = new();
    public static List<SpecificationAttributeOption> specOptions = new();
    public static List<ProductAttributeCombination> productAttributeCombinations = new();
    public static List<RelatedProduct> relatedProducts = new();
    public void OnGet()
    {
    var allProducts = GetDataFromDb.GetProductsAsync().Result;
        products.AddRange(allProducts);

        allProducts.ForEach(prod =>
               attributeMappings.AddRange(GetDataFromDb.GetProductAttributeMappingsAsync(prod.Id)
                                .Result)
                            );

        attributeMappings.ForEach(map =>
                           attrs.AddRange(GetDataFromDb.GetProductAttributesAsync(map.ProductId)
                                .Result)
                         );

        attributeMappings.ForEach(map =>
                      attrValues.AddRange(GetDataFromDb.GetProductAttributeValuesAsync(map.Id)
                                .Result)
                 );

        allProducts.ForEach(prod =>
                    specMappings.AddRange(GetDataFromDb.GetProductSpecificationAttributeMappingsAsync(prod.Id)
                                .Result)
                     );

        specMappings.ForEach(map =>
                     specOptions.AddRange(GetDataFromDb.GetSpecificationAttributeOptionsAsync(map.SpecificationAttributeOptionId)
                                .Result)
             );

        specOptions.ForEach(spec =>
                           specs.AddRange(GetDataFromDb.GetSpecificationAttributesAsync(spec.SpecificationAttributeId)
                                .Result)
                           );
        products.ForEach(prod =>
                 relatedProducts.AddRange(GetDataFromDb.GetRelatedProductsAsync(prod.Id)
                                .Result)
                   );

        Console.WriteLine();
        //Console.WriteLine("public List<SpecificationAttribute> = ");
        Console.WriteLine("const specificationAttributes = ");
        var json = GetDataFromDb.SerializeToJson(specs);
        // Console.WriteLine("Specifications");
        Console.WriteLine(json + ";");

        json = GetDataFromDb.SerializeToJson(specOptions);
        Console.WriteLine();
        //Console.WriteLine("public List<SpecificationAttributeOption> = ");
        Console.WriteLine("const specificationAttributeOptions = ");
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<Product> = ");
        Console.WriteLine("const products = ");
        json = GetDataFromDb.SerializeToJson(allProducts);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<Product> = ");
        Console.WriteLine("const relatedProducts = ");
        json = GetDataFromDb.SerializeToJson(relatedProducts);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<ProductAttribute> = ");
        Console.WriteLine("const productAttributes = ");
        json = GetDataFromDb.SerializeToJson(attrs);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        // Console.WriteLine("public List<ProductAttributeValue> = ");
        Console.WriteLine("const productAttributeValues = ");
        json = GetDataFromDb.SerializeToJson(attrValues);
        Console.WriteLine(json + ";");


        Console.WriteLine();
        //Console.WriteLine("public List<Product_ProductAttribute_Mapping> = ");
        Console.WriteLine("const product_ProductAttribute_Mappings = ");
        json = GetDataFromDb.SerializeToJson(attributeMappings);
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<Product_SpecificationAttribute_Mapping> = ");
        Console.WriteLine("const product_SpecificationAttribute_Mappings = ");
        json = GetDataFromDb.SerializeToJson(specMappings);
        Console.WriteLine(json + ";");

        Console.WriteLine("✅ Extraction complete!");
    }
}
