using ProductPrototype.DbAccess.Data.ModelDto;

namespace ProductPrototype.DbAccess.Interfaces;

public interface IGetDataFromDb
{
    Task<List<ProductDetailDto>> GetProductsAsync(int parentId, string name = "");
}