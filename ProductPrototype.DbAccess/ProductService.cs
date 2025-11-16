namespace ProductPrototype.Models;

using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Data.Models;
using ProductPrototype.DbAccess.ModelDTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Make sure to use the namespace where your CustomNopContext is defined


public class ProductService // This is in your Data Access Library
{
    private readonly RepositoryContext _context;

    // The context is injected via Dependency Injection
    public ProductService(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDetailDto>> GetProductAttributeDetailsAsync(int parentId)
    {
        // ⭐️ The query is executed against the injected _context object ⭐️
        var productAttributeDetails = await (from product in _context.Products
                                                 // Inner Join setup
                                             join mapping in _context.ProductSpecificationAttributeMappings on product.Id equals mapping.ProductId 
                                             join parent in _context.Products  on mapping.ProductId equals parent.Id
                                             join spcOpt in _context.SpecificationAttributeOptions on mapping.SpecificationAttributeOptionId equals spcOpt.Id
                                             join spcOpt2 in _context.SpecificationAttributeOptions on mapping.SpecificationAttributeOptionId equals spcOpt2.Id

                                             join spec in _context.SpecificationAttributes on spcOpt.SpecificationAttributeId equals spec.Id
                                             join specGr in _context.SpecificationAttributeGroups on spec.SpecificationAttributeGroupId equals specGr.Id
                                            where parent.Id == parentId || product.Id == parentId 
                                             // Project the results into a clean DTO
                                             select new ProductDetailDto
                                             {
                                                 ProductId = product.Id,
                                                 ProductName = product.Name,
                                                 Sku = product.Sku,
                                                 Price = product.Price,
                                                 Weight = product.Weight,
                                                 GroupBySpecificationId = specGr.Id,
                                                 SpecificationOptionName = spcOpt.Name

                                             })
                                            .ToListAsync(); // Executes the query and returns the results

        return productAttributeDetails;
    }
}
