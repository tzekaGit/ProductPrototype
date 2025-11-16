using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("BackInStockSubscription")]
[Index("CustomerId", Name = "IX_BackInStockSubscription_CustomerId")]
[Index("ProductId", Name = "IX_BackInStockSubscription_ProductId")]
public partial class BackInStockSubscription
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int StoreId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("BackInStockSubscriptions")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("BackInStockSubscriptions")]
    public virtual Product Product { get; set; } = null!;
}
