using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Struct;

namespace ProductPrototype.Pages;

public class IndexModel : PageModel
{
    public List<ProductDetailDto> ProductDetails { get; set; }
    public ProductTemplates ProductTemplates { get; set; }
    private readonly GetDataFromDb _getDataFromDb;

    private readonly HtmlProductExtractor _htmlProductExtractor;

    public IndexModel(GetDataFromDb getDataFromDb, HtmlProductExtractor htmlProductExtractor)
    {
        _getDataFromDb = getDataFromDb;
        _htmlProductExtractor = htmlProductExtractor;
        ProductTemplates = new ProductTemplates();
    }

    public async Task OnGetAsync()
    {
        var parentid = 4;
       // await _htmlProductExtractor.ExtractAllProductsFromHtmlAsync();

        var productDetails = await _getDataFromDb.GetProductsAsync(parentid);
        ProductDetails = productDetails;

        Console.WriteLine("✅ Extraction complete!");
    }
}
