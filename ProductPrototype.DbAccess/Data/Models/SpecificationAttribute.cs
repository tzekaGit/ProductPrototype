using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("SpecificationAttribute")]
[Index("SpecificationAttributeGroupId", Name = "IX_SpecificationAttribute_SpecificationAttributeGroupId")]
public partial class SpecificationAttribute
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? SpecificationAttributeGroupId { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("SpecificationAttributeGroupId")]
    [InverseProperty("SpecificationAttributes")]
    public virtual SpecificationAttributeGroup? SpecificationAttributeGroup { get; set; }

    [InverseProperty("SpecificationAttribute")]
    public virtual ICollection<SpecificationAttributeOption> SpecificationAttributeOptions { get; set; } = new List<SpecificationAttributeOption>();
}
