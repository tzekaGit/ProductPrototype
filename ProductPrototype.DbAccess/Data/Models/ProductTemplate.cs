using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductTemplate")]
public partial class ProductTemplate
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string ViewPath { get; set; } = null!;
    public string TemplateStyle { get; set; } = null!;
    public int DisplayOrder { get; set; }

    public string? IgnoredProductTypes { get; set; }
}
