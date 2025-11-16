using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductAttributeCombination")]
[Index("ProductId", Name = "IX_ProductAttributeCombination_ProductId")]
[Index("Sku", Name = "IX_ProductAttributeCombination_Sku")]
public partial class ProductAttributeCombination
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string? Sku { get; set; }

    [StringLength(400)]
    public string? ManufacturerPartNumber { get; set; }

    [StringLength(400)]
    public string? Gtin { get; set; }

    public int ProductId { get; set; }

    public string? AttributesXml { get; set; }

    public int StockQuantity { get; set; }

    public bool AllowOutOfStockOrders { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? OverriddenPrice { get; set; }

    public int NotifyAdminForQuantityBelow { get; set; }

    public int MinStockQuantity { get; set; }

    public int? PictureId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductAttributeCombinations")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("ProductAttributeCombination")]
    public virtual ICollection<ProductAttributeCombinationPicture> ProductAttributeCombinationPictures { get; set; } = new List<ProductAttributeCombinationPicture>();
}
