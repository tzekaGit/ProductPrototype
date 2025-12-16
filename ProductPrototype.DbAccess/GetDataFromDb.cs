using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Interfaces;
using ProductPrototype.DbAccess.Services;

namespace ProductPrototype.DbAccess;

public class GetDataFromDb : IGetDataFromDb
{
    readonly ProductService _productService;
    public GetDataFromDb(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductDetailDto>> GetProductsAsync(int parentId, string name = "")
    {
        using (var context = new RepositoryContext())
        {
            try
            {
                Console.WriteLine("Executing EF Core 10 Query...");
                // LINQ query: The filtering is translated into a 'WHERE' clause in SQL
                var productDetails = await _productService.GetProductDetailsAsync(parentId, name);
                foreach (var prod in productDetails)
                {
                    if (prod.ParentGroupedProductId != 0)
                    {
                        prod.ProductAttributes = _productService.GetProductAttributesAsync(prod.Id).Result;
                        prod.ProductSpecificationAttributeOptions = _productService.GetProductSpecificationsAsync(prod.ProductId).Result;
                        prod.RelatedProducts = await _productService.GetRelatedProductsAsync(prod.Id);
                        prod.RelatedProductDetails = await _productService.GetRelatedProductDetailAsync(prod.Id);
                        prod.ProductTemplate = await _productService.GetProductTemplateAsync(prod.Id);
                    }
                    else
                    {
                        prod.ProductTemplate = await _productService.GetProductTemplateAsync(parentId);
                    }

                }

                return productDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }

}