using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;
using ProductPrototype.DbAccess.Interfaces;
// Make sure to use the namespace where your CustomNopContext is defined

namespace ProductPrototype.DbAccess.Services;

public class ProductService : IProductService
// This is in your Data Access Library
{
    private readonly RepositoryContext _context;

    // The _context is injected via Dependency Injection
    public ProductService(RepositoryContext context)
    {
        _context = context;
    }
 
    public async Task<List<ProductDetailDto>> GetProductDetailsAsync(int parentId)
    {
        // ⭐️ The query is executed against the injected _context object ⭐️
        var productDetails = await (from product in _context.Products
                                        // Inner Join setup
                                    join mapping in _context.ProductSpecificationAttributeMappings on product.Id equals mapping.ProductId
                                    join spcOpt in _context.SpecificationAttributeOptions on mapping.SpecificationAttributeOptionId equals spcOpt.Id
                                    join spec in _context.SpecificationAttributes on spcOpt.SpecificationAttributeId equals spec.Id
                                    join specGr in _context.SpecificationAttributeGroups on spec.SpecificationAttributeGroupId equals specGr.Id
                                    where (product.Id == parentId || product.ParentGroupedProductId == parentId) 
                                        && specGr.Name == "ProductGrouping1"
                                    orderby  spcOpt.DisplayOrder ascending, product.DisplayOrder ascending
                                    // Project the results into a clean DTO
                                    select new ProductDetailDto
                                            {
                                                Id = product.Id,
                                                ProductId = product.Id,
                                                ParentGroupedProductId = product.Id == parentId? 0 : parentId,
                                                Name = product.Name,
                                                Sku = product.Sku,
                                                Price = product.Price,
                                                Weight = product.Weight,
                                                Width = product.Width,
                                                Length = product.Length,
                                                Height = product.Height,
                                                FullDescription = product.FullDescription,
                                                ShortDescription = product.ShortDescription,
                                                GroupBySpecificationId = specGr.Id,
                                                SpecificationOptionName = spcOpt.Name
                                    })
                                    .ToListAsync(); // Executes the query and returns the results

        return productDetails;
    }


    public async Task<List<ProductAttributeDto>> GetProductAttributesAsync(int productId)
    {
        var productAttributes = await (from prod in _context.Products
                                       join map in _context.ProductProductAttributeMappings on prod.Id equals map.ProductId
                                       join attr in _context.ProductAttributes on map.ProductAttributeId equals attr.Id
                                       where prod.Id == productId
                                       orderby map.DisplayOrder ascending
                                       select new ProductAttributeDto
                                       {
                                         AttributeControlTypeId = map.AttributeControlTypeId,
                                         ProductAttributeId = attr.Id,
                                         ProductAttributeName = attr.Name
                                       })
                                       .ToListAsync();


        foreach (var productAttribute in productAttributes)
        {
        var ProductAttributeValues = await (from attr in _context.ProductAttributes
                                           join map in _context.ProductProductAttributeMappings on attr.Id equals map.ProductAttributeId
                                           join attValue in _context.ProductAttributeValues on map.Id equals attValue.ProductAttributeMappingId
                                           join prod in _context.Products on map.ProductId equals prod.Id
                                           where (prod.Id == productId && attr.Id == productAttribute.ProductAttributeId)
                                           orderby attValue.DisplayOrder ascending
                                            select new ProductAttributeValue
                                         {
                                            Id = attValue.Id,
                                            Name = attValue.Name,
                                            DisplayOrder = attValue.DisplayOrder,
                                         })
                                     .ToListAsync();
            productAttribute.ProductAttributeValues = ProductAttributeValues;

        }

        return productAttributes;
    }
    public async Task<List<SpecificationAttributeOptionDto>> GetProductSpecificationsAsync(int productId)
    {
        var productSpecifications = await (from product in _context.Products
                                               // Inner Join setup
                                        join mapping in _context.ProductSpecificationAttributeMappings on product.Id equals mapping.ProductId
                                        join spcOpt in _context.SpecificationAttributeOptions on mapping.SpecificationAttributeOptionId equals spcOpt.Id
                                        join spec in _context.SpecificationAttributes on spcOpt.SpecificationAttributeId equals spec.Id
                                        join specGr in _context.SpecificationAttributeGroups on spec.SpecificationAttributeGroupId equals specGr.Id
                                        where product.Id == productId && specGr.Name == "TableColumn1"
                                        orderby spcOpt.DisplayOrder ascending

                                           // Project the results into a clean DTO
                                        select new SpecificationAttributeOptionDto
                                            {
                                                SpecificationId = spcOpt.Id,
                                                SpecificationAttributeId = spec.Id,
                                                SpecificationName = spcOpt.Name,
                                                SpecificationKey = specGr.Name
                                        
                                            })
                                            .Distinct()
                                            .ToListAsync(); // Executes the query and returns the results

        return productSpecifications;
    }

    public async Task<List<RelatedProduct>> GetRelatedProductsAsync(int ProductId)
    {
        return  _context.RelatedProducts.Select(p => p).Where(filter=> filter.ProductId1 == ProductId).ToList();
    }
}
