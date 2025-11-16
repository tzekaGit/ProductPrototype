using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductWarehouseInventory")]
[Index("ProductId", Name = "IX_ProductWarehouseInventory_ProductId")]
[Index("WarehouseId", Name = "IX_ProductWarehouseInventory_WarehouseId")]
public partial class ProductWarehouseInventory
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int StockQuantity { get; set; }

    public int ReservedQuantity { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductWarehouseInventories")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("WarehouseId")]
    [InverseProperty("ProductWarehouseInventories")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
