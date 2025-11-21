using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.Pages;

public class IndexModel : PageModel
{
    public List<ProductDetailDto> ProductDetails { get; set; }

    private readonly GetDataFromDb _getDataFromDb;
    public IndexModel(GetDataFromDb getDataFromDb)
    {
        _getDataFromDb = getDataFromDb;
    }

    public async Task OnGetAsync()
    {
        var parentid = 48;
       var productDetails = await _getDataFromDb.GetProductsAsync(parentid);


        ProductDetails = productDetails;

        Console.WriteLine("✅ Extraction complete!");
    }
}
