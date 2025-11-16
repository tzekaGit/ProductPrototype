using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Order")]
[Index("OrderGuid", Name = "AK_Order_OrderGuid", IsUnique = true)]
[Index("BillingAddressId", Name = "IX_Order_BillingAddressId")]
[Index("CreatedOnUtc", Name = "IX_Order_CreatedOnUtc", AllDescending = true)]
[Index("CustomerId", Name = "IX_Order_CustomerId")]
[Index("PickupAddressId", Name = "IX_Order_PickupAddressId")]
[Index("ShippingAddressId", Name = "IX_Order_ShippingAddressId")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    public string CustomOrderNumber { get; set; } = null!;

    public int BillingAddressId { get; set; }

    public int CustomerId { get; set; }

    public int? PickupAddressId { get; set; }

    public int? ShippingAddressId { get; set; }

    [StringLength(100)]
    public string? CustomerIp { get; set; }

    public Guid OrderGuid { get; set; }

    public int StoreId { get; set; }

    public bool PickupInStore { get; set; }

    public int OrderStatusId { get; set; }

    public int ShippingStatusId { get; set; }

    public int PaymentStatusId { get; set; }

    public string? PaymentMethodSystemName { get; set; }

    public string? CustomerCurrencyCode { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal CurrencyRate { get; set; }

    public int CustomerTaxDisplayTypeId { get; set; }

    public string? VatNumber { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderSubtotalInclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderSubtotalExclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderSubTotalDiscountInclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderSubTotalDiscountExclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderShippingInclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderShippingExclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PaymentMethodAdditionalFeeInclTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PaymentMethodAdditionalFeeExclTax { get; set; }

    public string? TaxRates { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderTax { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderDiscount { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderTotal { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal RefundedAmount { get; set; }

    public int? RewardPointsHistoryEntryId { get; set; }

    public string? CheckoutAttributeDescription { get; set; }

    public string? CheckoutAttributesXml { get; set; }

    public int CustomerLanguageId { get; set; }

    public int AffiliateId { get; set; }

    public bool AllowStoringCreditCardNumber { get; set; }

    public string? CardType { get; set; }

    public string? CardName { get; set; }

    public string? CardNumber { get; set; }

    public string? MaskedCreditCardNumber { get; set; }

    public string? CardCvv2 { get; set; }

    public string? CardExpirationMonth { get; set; }

    public string? CardExpirationYear { get; set; }

    public string? AuthorizationTransactionId { get; set; }

    public string? AuthorizationTransactionCode { get; set; }

    public string? AuthorizationTransactionResult { get; set; }

    public string? CaptureTransactionId { get; set; }

    public string? CaptureTransactionResult { get; set; }

    public string? SubscriptionTransactionId { get; set; }

    [Precision(6)]
    public DateTime? PaidDateUtc { get; set; }

    public string? ShippingMethod { get; set; }

    public string? ShippingRateComputationMethodSystemName { get; set; }

    public string? CustomValuesXml { get; set; }

    public bool Deleted { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    public int? RedeemedRewardPointsEntryId { get; set; }

    [ForeignKey("BillingAddressId")]
    [InverseProperty("OrderBillingAddresses")]
    public virtual Address BillingAddress { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; } = new List<DiscountUsageHistory>();

    [InverseProperty("UsedWithOrder")]
    public virtual ICollection<GiftCardUsageHistory> GiftCardUsageHistories { get; set; } = new List<GiftCardUsageHistory>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderNote> OrderNotes { get; set; } = new List<OrderNote>();

    [ForeignKey("PickupAddressId")]
    [InverseProperty("OrderPickupAddresses")]
    public virtual Address? PickupAddress { get; set; }

    [InverseProperty("InitialOrder")]
    public virtual ICollection<RecurringPayment> RecurringPayments { get; set; } = new List<RecurringPayment>();

    [ForeignKey("RewardPointsHistoryEntryId")]
    [InverseProperty("Orders")]
    public virtual RewardPointsHistory? RewardPointsHistoryEntry { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    [ForeignKey("ShippingAddressId")]
    [InverseProperty("OrderShippingAddresses")]
    public virtual Address? ShippingAddress { get; set; }
}
