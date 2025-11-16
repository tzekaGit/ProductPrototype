using HtmlExtractor.Models;
using System.Text.Json;

namespace ProductPrototype.DbAccess;

public static class ProductExtractorFromDb
{
    //public static List<ProductHeader> headers = new();
    public static List<Product> products = new();
    public static List<ProductProductAttributeMapping> attributeMappings = new();
    public static List<ProductAttribute> attrs = new();
    public static List<ProductAttributeValue> attrValues = new();
    public static List<SpecificationAttribute> specs = new();
    public static List<ProductSpecificationAttributeMapping> specMappings = new();
    public static List<SpecificationAttributeOption> specOptions = new();
    public static List<ProductAttributeCombination> productAttributeCombinations = new();
    public static List<RelatedProduct> relatedProducts = new();


    public static void ExtractAllProductsFromDb()
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
       var  json = SerializeToJson(specs);
        // Console.WriteLine("Specifications");
        Console.WriteLine(json + ";");

        json = SerializeToJson(specOptions);
        Console.WriteLine();
        //Console.WriteLine("public List<SpecificationAttributeOption> = ");
        Console.WriteLine("const specificationAttributeOptions = ");
        Console.WriteLine(json + ";");

        Console.WriteLine();
        //Console.WriteLine("public List<Product> = ");
        Console.WriteLine("const products = ");
        json = SerializeToJson(allProducts);
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

 
}

