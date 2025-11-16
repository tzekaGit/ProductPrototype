using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("TierPrice")]
[Index("CustomerRoleId", Name = "IX_TierPrice_CustomerRoleId")]
[Index("ProductId", Name = "IX_TierPrice_ProductId")]
public partial class TierPrice
{
    [Key]
    public int Id { get; set; }

    public int? CustomerRoleId { get; set; }

    public int ProductId { get; set; }

    public int StoreId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    [Precision(6)]
    public DateTime? StartDateTimeUtc { get; set; }

    [Precision(6)]
    public DateTime? EndDateTimeUtc { get; set; }

    [ForeignKey("CustomerRoleId")]
    [InverseProperty("TierPrices")]
    public virtual CustomerRole? CustomerRole { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TierPrices")]
    public virtual Product Product { get; set; } = null!;
}
