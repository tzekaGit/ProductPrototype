using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("DiscountUsageHistory")]
[Index("DiscountId", Name = "IX_DiscountUsageHistory_DiscountId")]
[Index("OrderId", Name = "IX_DiscountUsageHistory_OrderId")]
public partial class DiscountUsageHistory
{
    [Key]
    public int Id { get; set; }

    public int DiscountId { get; set; }

    public int OrderId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("DiscountId")]
    [InverseProperty("DiscountUsageHistories")]
    public virtual Discount Discount { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("DiscountUsageHistories")]
    public virtual Order Order { get; set; } = null!;
}
