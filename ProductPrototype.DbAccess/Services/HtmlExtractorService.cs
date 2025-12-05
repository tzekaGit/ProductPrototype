using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess.Services;

internal class HtmlExtractorService
{

    private readonly RepositoryContext _context;

    // The _context is injected via Dependency Injection
    public HtmlExtractorService(RepositoryContext context)
    {
        _context = context;
    }
    public async Task<bool> InsertProductAttributeAsync(List<ProductAttribute> productAttributes)
    {
        foreach (var attribute in productAttributes)
        {

            _context.ProductAttributes.Add(attribute);
        }
        return true;
    }
    public async Task<bool> InsertSpecificationAttributeGroupAsync(SpecificationAttributeGroup productSpecificationAttributeGroup)
    {
        _context.SpecificationAttributeGroups.Add(productSpecificationAttributeGroup);
        return true;
    }

    public async Task<bool> InsertSpecificationAttributeAsync(SpecificationAttribute specificationAttribute)
    {
        _context.SpecificationAttributes.Add(specificationAttribute);
        return true;
    }
    public async Task<bool> InsertSpecificationAttributeOptionsAsync(List<SpecificationAttributeOption> specificationAttributeOptions)
    {
        foreach (var specificationAttributeOption in specificationAttributeOptions)
        {
            _context.SpecificationAttributeOptions.Add(specificationAttributeOption);
        }
        return true;
    }

    public async Task<bool> InsertProductsAsync(List<Product> products)
    {
        foreach (var product in products)
        {
            _context.Products.Add(product);
        }
        return true;
    }

    public async Task<bool> InsertProductCategoryMappingAsync(List<ProductCategoryMapping> productCategoryMappings)
    {
        foreach (var productCategoryMapping in productCategoryMappings)
        {
            _context.ProductCategoryMappings.Add(productCategoryMapping);
        }
        return true;
    }

    public async Task<bool> InsertProductProductAttributeMappingsAsync(List<ProductProductAttributeMapping> productProductAttributeMappings)
    {
        foreach (var productProductAttributeMapping in productProductAttributeMappings)
        {
            _context.ProductProductAttributeMappings.Add(productProductAttributeMapping);
        }
        return true;
    }

    public async Task<bool> InsertProductAttributeValuesAsync(List<ProductAttributeValue> productAttributeValues)
    {
        foreach (var productAttributeValue in productAttributeValues)
        {
            _context.ProductAttributeValues.Add(productAttributeValue);
        }
        return true;
    }

    public async Task<bool> InsertProductSpecificationAttributeMappingsAsync(List<ProductSpecificationAttributeMapping> productSpecificationAttributeMappings)
    {
        foreach (var productSpecificationAttributeMapping in productSpecificationAttributeMappings)
        {
            _context.ProductSpecificationAttributeMappings.Add(productSpecificationAttributeMapping);
        }
        return true;
    }

    public async Task<bool> InsertRelatedProductsAsync(List<RelatedProduct> pelatedProducts)
    {
        foreach (var pelatedProduct in pelatedProducts)
        {
            _context.RelatedProducts.Add(pelatedProduct);
        }
        return true;
    }
}
