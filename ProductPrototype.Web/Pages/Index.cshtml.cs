using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Data.ModelDto;

namespace ProductPrototype.Pages;

public class IndexModel : PageModel
{
    public List<ProductDetailDto> ProductDetails { get; set; }
    public string TblTemlate { get; set; }
    public string HeaderTemlate { get; set; }


    private readonly GetDataFromDb _getDataFromDb;

    private readonly HtmlProductExtractor _htmlProductExtractor;

    public IndexModel(GetDataFromDb getDataFromDb, HtmlProductExtractor htmlProductExtractor)
    {
        _getDataFromDb = getDataFromDb;
        _htmlProductExtractor = htmlProductExtractor;
    }

    public async Task OnGetAsync()
    {
        int parentid = 4;// 62;37; 22; 4;22
        parentid = !string.IsNullOrEmpty(Request.Query["id"]) ? int.Parse(Request.Query["id"]) : parentid;
        var name = Request.Query["name"].ToString() ?? "";
        var url = Request.Query["url"].ToString() ?? "";

        if (!string.IsNullOrEmpty(url))
        {
            await _htmlProductExtractor.ExtractAllProductsFromHtmlAsync(url);
            ProductDetails = await _getDataFromDb.GetProductsAsync(parentid, url);
        }
        else if (!string.IsNullOrEmpty(name))
        {
            ProductDetails = await _getDataFromDb.GetProductsAsync(0, name);
        }
        else if (parentid > 0)
        {
            ProductDetails = await _getDataFromDb.GetProductsAsync(parentid);
        }
        HeaderTemlate = ProductDetails?
                        .FirstOrDefault(prod => prod.ParentGroupedProductId == 0 || prod.Id == parentid)
                        ?.ProductTemplate?.ViewPath;

        Console.WriteLine("✅ Extraction complete!");
    }
}
