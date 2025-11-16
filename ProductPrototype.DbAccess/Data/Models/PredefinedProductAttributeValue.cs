using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("PredefinedProductAttributeValue")]
[Index("ProductAttributeId", Name = "IX_PredefinedProductAttributeValue_ProductAttributeId")]
public partial class PredefinedProductAttributeValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public int ProductAttributeId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceAdjustment { get; set; }

    public bool PriceAdjustmentUsePercentage { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal WeightAdjustment { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Cost { get; set; }

    public bool IsPreSelected { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("ProductAttributeId")]
    [InverseProperty("PredefinedProductAttributeValues")]
    public virtual ProductAttribute ProductAttribute { get; set; } = null!;
}
