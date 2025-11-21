using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.Pages;

public class IndexModel2 : PageModel
{
    private readonly GetDataFromDb _getDataFromDb;
    public IndexModel2(GetDataFromDb getDataFromDb)
    {
        _getDataFromDb = getDataFromDb;
    }

    public static List<ProductDetailDto> ProductDetails { get; set; }

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
        var parentid = 48;
         //ProductDetails = _getDataFromDb.GetProductsAsync(parentid).Result;


        Console.WriteLine("✅ Extraction complete!");
    }
}
