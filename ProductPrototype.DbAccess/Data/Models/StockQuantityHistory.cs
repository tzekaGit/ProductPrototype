using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("StockQuantityHistory")]
[Index("ProductId", Name = "IX_StockQuantityHistory_ProductId")]
public partial class StockQuantityHistory
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int QuantityAdjustment { get; set; }

    public int StockQuantity { get; set; }

    public string? Message { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    public int? CombinationId { get; set; }

    public int? WarehouseId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("StockQuantityHistories")]
    public virtual Product Product { get; set; } = null!;
}
