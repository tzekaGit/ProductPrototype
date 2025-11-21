using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.Models;
using System.Threading.Tasks;

namespace ProductPrototype.DbAccess.Interfaces
{
    public interface IGetDataFromDb
    {
        static abstract Task<List<ProductProductAttributeMapping>> GetProductAttributeMappingsAsync(int productId);
        static abstract Task<List<ProductAttribute>> GetProductAttributesAsync(int ProductAttributeId);
        static abstract Task<List<ProductAttributeValue>> GetProductAttributeValuesAsync(int ProductAttributeMappingId);
        static abstract Task<List<ProductSpecificationAttributeMapping>> GetProductSpecificationAttributeMappingsAsync(int productId);
        static abstract Task<List<RelatedProduct>> GetRelatedProductsAsync(int productId);
        static abstract Task<List<SpecificationAttributeOption>> GetSpecificationAttributeOptionsAsync(int specificationAttributeOptionId);
        static abstract Task<List<SpecificationAttribute>> GetSpecificationAttributesAsync(int specificationAttributeId);
        static abstract string SerializeToJson(object data);

    }
}