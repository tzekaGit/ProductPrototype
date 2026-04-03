using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.Pages;

public class IndexModel : PageModel
{
    public List<ProductDetailDto> ProductDetails { get; set; }
    public string TblTemlate { get; set; }
    public string HeaderTemlate { get; set; }
    public string Navigations { get; set; }
    public List<ProductTemplate> ProductTemplate { get; set; }



    private readonly GetDataFromDb _getDataFromDb;

    private readonly HtmlProductExtractor _htmlProductExtractor;

    public IndexModel(GetDataFromDb getDataFromDb, HtmlProductExtractor htmlProductExtractor)
    {
        _getDataFromDb = getDataFromDb;
        _htmlProductExtractor = htmlProductExtractor;
    }

    public async Task OnGetAsync()
    {
        int parentid = 1;// 62;37; 22; 4;22
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
        if (ProductDetails != null)
        {
            HeaderTemlate = ProductDetails?
                                .FirstOrDefault(prod => prod.ParentGroupedProductId == 0 || prod.Id == parentid)
                               ?.ProductTemplate
                               ?.TemplateStyle;

            var navs = (await _getDataFromDb.GetAllProductsAsync())
                              .Select(p => new
                              {
                                  Name = p.Name,
                                  Id = p.Id,
                                  Sku = p.Sku,
                              }).ToList();
            Navigations = _htmlProductExtractor.SerializeToJson(navs);

            ProductTemplate = ProductDetails
                                .Where(prod => prod.ParentGroupedProductId == parentid || prod.Id == parentid)
                                .Select(prod => prod.ProductTemplate)
                                .ToList();

        }

        Console.WriteLine("✅ Extraction complete!");
    }
}
