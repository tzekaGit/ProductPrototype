using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess.Data.ModelDto;

public class ProductDetailDto
{

    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ParentGroupedProductId { get; set; }
    public string Name { get; set; }
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public decimal OldPrice { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public decimal Height { get; set; }
    public string FullDescription { get; set; }
    public string ShortDescription { get; set; }
    public int GroupBySpecificationId { get; set; }
    public string SpecificationOptionName { get; set; }


    public bool IsRequired { get; set; }
    public List<RelatedProduct>? RelatedProducts { get; set; }
    public List<ProductDetailDto>? RelatedProductDetails { get; set; }
    public List<ProductAttributeDto>? ProductAttributes { get; set; }
    public List<SpecificationAttributeOptionDto>? ProductSpecificationAttributeOptions { get; set; }
    public ProductTemplate? ProductTemplate { get; set; }

}
