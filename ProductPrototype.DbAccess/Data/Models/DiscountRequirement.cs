using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("DiscountRequirement")]
[Index("DiscountId", Name = "IX_DiscountRequirement_DiscountId")]
[Index("ParentId", Name = "IX_DiscountRequirement_ParentId")]
public partial class DiscountRequirement
{
    [Key]
    public int Id { get; set; }

    public int DiscountId { get; set; }

    public int? ParentId { get; set; }

    public string? DiscountRequirementRuleSystemName { get; set; }

    public int? InteractionTypeId { get; set; }

    public bool IsGroup { get; set; }

    [ForeignKey("DiscountId")]
    [InverseProperty("DiscountRequirements")]
    public virtual Discount Discount { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<DiscountRequirement> InverseParent { get; set; } = new List<DiscountRequirement>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual DiscountRequirement? Parent { get; set; }
}
