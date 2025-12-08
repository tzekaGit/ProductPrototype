using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess.Data.ModelDto;

public class ProductAttributeDto
{
    public int ProductId { get; set; }
    public int AttributeControlTypeId { get; set; }
    public int ProductAttributeId { get; set; }
    public string? ProductAttributeName { get; set; }
    public List<ProductAttributeValue>? ProductAttributeValues { get; set; }
}
