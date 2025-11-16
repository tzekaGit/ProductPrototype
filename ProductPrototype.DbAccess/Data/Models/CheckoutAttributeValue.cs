using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("CheckoutAttributeValue")]
[Index("CheckoutAttributeId", Name = "IX_CheckoutAttributeValue_CheckoutAttributeId")]
public partial class CheckoutAttributeValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? ColorSquaresRgb { get; set; }

    public int CheckoutAttributeId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceAdjustment { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal WeightAdjustment { get; set; }

    public bool IsPreSelected { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("CheckoutAttributeId")]
    [InverseProperty("CheckoutAttributeValues")]
    public virtual CheckoutAttribute CheckoutAttribute { get; set; } = null!;
}
