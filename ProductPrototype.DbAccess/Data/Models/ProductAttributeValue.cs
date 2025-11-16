using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductAttributeValue")]
[Index("ProductAttributeMappingId", Name = "IX_ProductAttributeValue_ProductAttributeMappingId")]
[Index("ProductAttributeMappingId", "DisplayOrder", Name = "IX_ProductAttributeValue_ProductAttributeMappingId_DisplayOrder")]
public partial class ProductAttributeValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? ColorSquaresRgb { get; set; }

    public int ProductAttributeMappingId { get; set; }

    public int AttributeValueTypeId { get; set; }

    public int AssociatedProductId { get; set; }

    public int ImageSquaresPictureId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceAdjustment { get; set; }

    public bool PriceAdjustmentUsePercentage { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal WeightAdjustment { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Cost { get; set; }

    public bool CustomerEntersQty { get; set; }

    public int Quantity { get; set; }

    public bool IsPreSelected { get; set; }

    public int DisplayOrder { get; set; }

    public int? PictureId { get; set; }

    [ForeignKey("ProductAttributeMappingId")]
    [InverseProperty("ProductAttributeValues")]
    public virtual ProductProductAttributeMapping ProductAttributeMapping { get; set; } = null!;

    [InverseProperty("ProductAttributeValue")]
    public virtual ICollection<ProductAttributeValuePicture> ProductAttributeValuePictures { get; set; } = new List<ProductAttributeValuePicture>();
}
