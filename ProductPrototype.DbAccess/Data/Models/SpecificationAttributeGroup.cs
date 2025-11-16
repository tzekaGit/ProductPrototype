using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("SpecificationAttributeGroup")]
public partial class SpecificationAttributeGroup
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DisplayOrder { get; set; }

    [InverseProperty("SpecificationAttributeGroup")]
    public virtual ICollection<SpecificationAttribute> SpecificationAttributes { get; set; } = new List<SpecificationAttribute>();
}
