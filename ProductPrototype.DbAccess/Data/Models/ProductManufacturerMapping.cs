using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Product_Manufacturer_Mapping")]
[Index("ProductId", "IsFeaturedProduct", Name = "IX_PMM_ProductId_Extended")]
[Index("ManufacturerId", "ProductId", Name = "IX_PMM_Product_and_Manufacturer")]
[Index("ManufacturerId", Name = "IX_ProductManufacturerMapping_ManufacturerId")]
[Index("IsFeaturedProduct", Name = "IX_Product_Manufacturer_Mapping_IsFeaturedProduct")]
[Index("ManufacturerId", Name = "IX_Product_Manufacturer_Mapping_ManufacturerId")]
[Index("ProductId", Name = "IX_Product_Manufacturer_Mapping_ProductId")]
public partial class ProductManufacturerMapping
{
    [Key]
    public int Id { get; set; }

    public int ManufacturerId { get; set; }

    public int ProductId { get; set; }

    public bool IsFeaturedProduct { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("ManufacturerId")]
    [InverseProperty("ProductManufacturerMappings")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductManufacturerMappings")]
    public virtual Product Product { get; set; } = null!;
}
