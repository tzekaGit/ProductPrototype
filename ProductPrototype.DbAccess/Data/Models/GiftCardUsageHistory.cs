using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("GiftCardUsageHistory")]
[Index("GiftCardId", Name = "IX_GiftCardUsageHistory_GiftCardId")]
[Index("UsedWithOrderId", Name = "IX_GiftCardUsageHistory_UsedWithOrderId")]
public partial class GiftCardUsageHistory
{
    [Key]
    public int Id { get; set; }

    public int GiftCardId { get; set; }

    public int UsedWithOrderId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal UsedValue { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("GiftCardId")]
    [InverseProperty("GiftCardUsageHistories")]
    public virtual GiftCard GiftCard { get; set; } = null!;

    [ForeignKey("UsedWithOrderId")]
    [InverseProperty("GiftCardUsageHistories")]
    public virtual Order UsedWithOrder { get; set; } = null!;
}
