using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductAttribute")]
public partial class ProductAttribute
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [InverseProperty("ProductAttribute")]
    public virtual ICollection<PredefinedProductAttributeValue> PredefinedProductAttributeValues { get; set; } = new List<PredefinedProductAttributeValue>();

    [InverseProperty("ProductAttribute")]
    public virtual ICollection<ProductProductAttributeMapping> ProductProductAttributeMappings { get; set; } = new List<ProductProductAttributeMapping>();
}
