using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("SpecificationAttributeOption")]
[Index("SpecificationAttributeId", Name = "IX_SpecificationAttributeOption_SpecificationAttributeId")]
public partial class SpecificationAttributeOption
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? ColorSquaresRgb { get; set; }

    public int SpecificationAttributeId { get; set; }

    public int DisplayOrder { get; set; }

    [InverseProperty("SpecificationAttributeOption")]
    public virtual ICollection<ProductSpecificationAttributeMapping> ProductSpecificationAttributeMappings { get; set; } = new List<ProductSpecificationAttributeMapping>();

    [ForeignKey("SpecificationAttributeId")]
    [InverseProperty("SpecificationAttributeOptions")]
    public virtual SpecificationAttribute SpecificationAttribute { get; set; } = null!;
}
