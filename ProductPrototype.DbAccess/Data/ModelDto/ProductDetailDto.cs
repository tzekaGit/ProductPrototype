using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess.Data.ModelDto;

public class ProductDetailDto: Product
{
    public int ProductId { get; set; }
    public int GroupBySpecificationId { get; set; }
    public int SpecificationOptionId { get; set; }
    public string? SpecificationOptionName { get; set; }
    public bool IsRequired { get; set; }
    public List<ProductAttributeDto>? ProductAttributes {get; set;}
    public List<SpecificationAttributeOptionDto>? ProductSpecificationAttributeOptions { get; set; }
}
