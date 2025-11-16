using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("GiftCard")]
[Index("PurchasedWithOrderItemId", Name = "IX_GiftCard_PurchasedWithOrderItemId")]
public partial class GiftCard
{
    [Key]
    public int Id { get; set; }

    public int? PurchasedWithOrderItemId { get; set; }

    public int GiftCardTypeId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Amount { get; set; }

    public bool IsGiftCardActivated { get; set; }

    public string? GiftCardCouponCode { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientEmail { get; set; }

    public string? SenderName { get; set; }

    public string? SenderEmail { get; set; }

    public string? Message { get; set; }

    public bool IsRecipientNotified { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [InverseProperty("GiftCard")]
    public virtual ICollection<GiftCardUsageHistory> GiftCardUsageHistories { get; set; } = new List<GiftCardUsageHistory>();

    [ForeignKey("PurchasedWithOrderItemId")]
    [InverseProperty("GiftCards")]
    public virtual OrderItem? PurchasedWithOrderItem { get; set; }
}
