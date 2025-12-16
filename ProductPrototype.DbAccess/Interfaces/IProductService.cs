using ProductPrototype.DbAccess.Data.ModelDto;

namespace ProductPrototype.DbAccess.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDetailDto>> GetProductDetailsAsync(int parentId, string name);
    }
}