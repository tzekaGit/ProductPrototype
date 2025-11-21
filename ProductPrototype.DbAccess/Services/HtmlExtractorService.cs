using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;
using ProductPrototype.DbAccess.Interfaces;

namespace ProductPrototype.DbAccess.Services;

internal class HtmlExtractorService
{
    private readonly RepositoryContext _context;

    // The context is injected via Dependency Injection
    public HtmlExtractorService(RepositoryContext context)
    {
        _context = context;
    }
    public async Task<int> GetProductDetailsAsync(int parentId)
    {
        // ⭐️ The query is executed against the injected _context object ⭐️
        var productDetails = await (from product in _context.Products);

        return 0;
    }
}
