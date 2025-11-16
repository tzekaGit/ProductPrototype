using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Customer")]
[Index("BillingAddressId", Name = "IX_Customer_BillingAddress_Id")]
[Index("CreatedOnUtc", Name = "IX_Customer_CreatedOnUtc", AllDescending = true)]
[Index("CurrencyId", Name = "IX_Customer_CurrencyId")]
[Index("CustomerGuid", Name = "IX_Customer_CustomerGuid")]
[Index("Deleted", Name = "IX_Customer_Deleted")]
[Index("Email", Name = "IX_Customer_Email")]
[Index("LanguageId", Name = "IX_Customer_LanguageId")]
[Index("ShippingAddressId", Name = "IX_Customer_ShippingAddress_Id")]
[Index("SystemName", Name = "IX_Customer_SystemName")]
[Index("Username", Name = "IX_Customer_Username")]
public partial class Customer
{
    [Key]
    public int Id { get; set; }

    [StringLength(1000)]
    public string? Username { get; set; }

    [StringLength(1000)]
    public string? Email { get; set; }

    [StringLength(1000)]
    public string? EmailToRevalidate { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [StringLength(1000)]
    public string? Gender { get; set; }

    [StringLength(1000)]
    public string? Company { get; set; }

    [StringLength(1000)]
    public string? StreetAddress { get; set; }

    [StringLength(1000)]
    public string? StreetAddress2 { get; set; }

    [StringLength(1000)]
    public string? ZipPostalCode { get; set; }

    [StringLength(1000)]
    public string? City { get; set; }

    [StringLength(1000)]
    public string? County { get; set; }

    [StringLength(1000)]
    public string? Phone { get; set; }

    [StringLength(1000)]
    public string? Fax { get; set; }

    [StringLength(1000)]
    public string? VatNumber { get; set; }

    [StringLength(1000)]
    public string? TimeZoneId { get; set; }

    [Column("CustomCustomerAttributesXML")]
    public string? CustomCustomerAttributesXml { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(400)]
    public string? SystemName { get; set; }

    [StringLength(100)]
    public string? LastIpAddress { get; set; }

    public int? CurrencyId { get; set; }

    public int? LanguageId { get; set; }

    [Column("BillingAddress_Id")]
    public int? BillingAddressId { get; set; }

    [Column("ShippingAddress_Id")]
    public int? ShippingAddressId { get; set; }

    public Guid CustomerGuid { get; set; }

    public int CountryId { get; set; }

    public int StateProvinceId { get; set; }

    public int VatNumberStatusId { get; set; }

    public int? TaxDisplayTypeId { get; set; }

    public string? AdminComment { get; set; }

    public bool IsTaxExempt { get; set; }

    public int AffiliateId { get; set; }

    public int VendorId { get; set; }

    public bool HasShoppingCartItems { get; set; }

    public bool RequireReLogin { get; set; }

    public int FailedLoginAttempts { get; set; }

    [Precision(6)]
    public DateTime? CannotLoginUntilDateUtc { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public bool IsSystemAccount { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime? LastLoginDateUtc { get; set; }

    [Precision(6)]
    public DateTime LastActivityDateUtc { get; set; }

    public int RegisteredInStoreId { get; set; }

    public bool MustChangePassword { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    [InverseProperty("Customer")]
    public virtual ICollection<BackInStockSubscription> BackInStockSubscriptions { get; set; } = new List<BackInStockSubscription>();

    [ForeignKey("BillingAddressId")]
    [InverseProperty("CustomerBillingAddresses")]
    public virtual Address? BillingAddress { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    [ForeignKey("CurrencyId")]
    [InverseProperty("Customers")]
    public virtual Currency? Currency { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<CustomWishlist> CustomWishlists { get; set; } = new List<CustomWishlist>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerPassword> CustomerPasswords { get; set; } = new List<CustomerPassword>();

    [InverseProperty("Customer")]
    public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords { get; set; } = new List<ExternalAuthenticationRecord>();

    [InverseProperty("Customer")]
    public virtual ICollection<ForumsPost> ForumsPosts { get; set; } = new List<ForumsPost>();

    [InverseProperty("FromCustomer")]
    public virtual ICollection<ForumsPrivateMessage> ForumsPrivateMessageFromCustomers { get; set; } = new List<ForumsPrivateMessage>();

    [InverseProperty("ToCustomer")]
    public virtual ICollection<ForumsPrivateMessage> ForumsPrivateMessageToCustomers { get; set; } = new List<ForumsPrivateMessage>();

    [InverseProperty("Customer")]
    public virtual ICollection<ForumsSubscription> ForumsSubscriptions { get; set; } = new List<ForumsSubscription>();

    [InverseProperty("Customer")]
    public virtual ICollection<ForumsTopic> ForumsTopics { get; set; } = new List<ForumsTopic>();

    [ForeignKey("LanguageId")]
    [InverseProperty("Customers")]
    public virtual Language? Language { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("Customer")]
    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Customer")]
    public virtual ICollection<PayPalToken> PayPalTokens { get; set; } = new List<PayPalToken>();

    [InverseProperty("Customer")]
    public virtual ICollection<PollVotingRecord> PollVotingRecords { get; set; } = new List<PollVotingRecord>();

    [InverseProperty("Customer")]
    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    [InverseProperty("Customer")]
    public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = new List<ReturnRequest>();

    [InverseProperty("Customer")]
    public virtual ICollection<RewardPointsHistory> RewardPointsHistories { get; set; } = new List<RewardPointsHistory>();

    [InverseProperty("Customer")]
    public virtual ICollection<Rfqquote> Rfqquotes { get; set; } = new List<Rfqquote>();

    [InverseProperty("Customer")]
    public virtual ICollection<RfqrequestQuote> RfqrequestQuotes { get; set; } = new List<RfqrequestQuote>();

    [ForeignKey("ShippingAddressId")]
    [InverseProperty("CustomerShippingAddresses")]
    public virtual Address? ShippingAddress { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    [InverseProperty("PmCustomer")]
    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Customers")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Customers")]
    public virtual ICollection<CustomerRole> CustomerRoles { get; set; } = new List<CustomerRole>();
}
