using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Product")]
[Index("Deleted", "VendorId", "ProductTypeId", "ManageInventoryMethodId", "MinStockQuantity", "UseMultipleWarehouses", Name = "IX_GetLowStockProducts")]
[Index("Deleted", "Id", Name = "IX_Product_Delete_Id")]
[Index("Published", "Deleted", Name = "IX_Product_Deleted_and_Published")]
[Index("LimitedToStores", Name = "IX_Product_LimitedToStores")]
[Index("Name", Name = "IX_Product_Name")]
[Index("ParentGroupedProductId", Name = "IX_Product_ParentGroupedProductId")]
[Index("Price", "AvailableStartDateTimeUtc", "AvailableEndDateTimeUtc", "Published", "Deleted", Name = "IX_Product_PriceDatesEtc")]
[Index("Published", Name = "IX_Product_Published")]
[Index("Name", "Sku", "ManufacturerPartNumber", "Deleted", Name = "IX_Product_Search")]
[Index("ShowOnHomepage", Name = "IX_Product_ShowOnHomepage")]
[Index("SubjectToAcl", Name = "IX_Product_SubjectToAcl")]
[Index("VisibleIndividually", Name = "IX_Product_VisibleIndividually")]
[Index("VisibleIndividually", "Published", "Deleted", Name = "IX_Product_VisibleIndividually_Published_Deleted_Extended")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string? MetaKeywords { get; set; }

    [StringLength(400)]
    public string? MetaTitle { get; set; }

    [StringLength(400)]
    public string? Sku { get; set; }

    [StringLength(400)]
    public string? ManufacturerPartNumber { get; set; }

    [StringLength(400)]
    public string? Gtin { get; set; }

    [StringLength(1000)]
    public string? RequiredProductIds { get; set; }

    [StringLength(1000)]
    public string? AllowedQuantities { get; set; }

    public int ProductTypeId { get; set; }

    public int ParentGroupedProductId { get; set; }

    public bool VisibleIndividually { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public string? AdminComment { get; set; }

    public int ProductTemplateId { get; set; }

    public int VendorId { get; set; }

    public bool ShowOnHomepage { get; set; }

    public string? MetaDescription { get; set; }

    public bool AllowCustomerReviews { get; set; }

    public int ApprovedRatingSum { get; set; }

    public int NotApprovedRatingSum { get; set; }

    public int ApprovedTotalReviews { get; set; }

    public int NotApprovedTotalReviews { get; set; }

    public bool SubjectToAcl { get; set; }

    public bool LimitedToStores { get; set; }

    public bool IsGiftCard { get; set; }

    public int GiftCardTypeId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? OverriddenGiftCardAmount { get; set; }

    public bool RequireOtherProducts { get; set; }

    public bool AutomaticallyAddRequiredProducts { get; set; }

    public bool IsDownload { get; set; }

    public int DownloadId { get; set; }

    public bool UnlimitedDownloads { get; set; }

    public int MaxNumberOfDownloads { get; set; }

    public int? DownloadExpirationDays { get; set; }

    public int DownloadActivationTypeId { get; set; }

    public bool HasSampleDownload { get; set; }

    public int SampleDownloadId { get; set; }

    public bool HasUserAgreement { get; set; }

    public string? UserAgreementText { get; set; }

    public bool IsRecurring { get; set; }

    public int RecurringCycleLength { get; set; }

    public int RecurringCyclePeriodId { get; set; }

    public int RecurringTotalCycles { get; set; }

    public bool IsRental { get; set; }

    public int RentalPriceLength { get; set; }

    public int RentalPricePeriodId { get; set; }

    public bool IsShipEnabled { get; set; }

    public bool IsFreeShipping { get; set; }

    public bool ShipSeparately { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal AdditionalShippingCharge { get; set; }

    public int DeliveryDateId { get; set; }

    public bool IsTaxExempt { get; set; }

    public int TaxCategoryId { get; set; }

    public int ManageInventoryMethodId { get; set; }

    public int ProductAvailabilityRangeId { get; set; }

    public bool UseMultipleWarehouses { get; set; }

    public int WarehouseId { get; set; }

    public int StockQuantity { get; set; }

    public bool DisplayStockAvailability { get; set; }

    public bool DisplayStockQuantity { get; set; }

    public int MinStockQuantity { get; set; }

    public int LowStockActivityId { get; set; }

    public int NotifyAdminForQuantityBelow { get; set; }

    public int BackorderModeId { get; set; }

    public bool AllowBackInStockSubscriptions { get; set; }

    public int OrderMinimumQuantity { get; set; }

    public int OrderMaximumQuantity { get; set; }

    public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }

    public bool DisplayAttributeCombinationImagesOnly { get; set; }

    public bool NotReturnable { get; set; }

    public bool DisableBuyButton { get; set; }

    public bool DisableWishlistButton { get; set; }

    public bool AvailableForPreOrder { get; set; }

    [Precision(6)]
    public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

    public bool CallForPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OldPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal ProductCost { get; set; }

    public bool CustomerEntersPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal MinimumCustomerEnteredPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal MaximumCustomerEnteredPrice { get; set; }

    public bool BasepriceEnabled { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal BasepriceAmount { get; set; }

    public int BasepriceUnitId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal BasepriceBaseAmount { get; set; }

    public int BasepriceBaseUnitId { get; set; }

    public bool MarkAsNew { get; set; }

    [Precision(6)]
    public DateTime? MarkAsNewStartDateTimeUtc { get; set; }

    [Precision(6)]
    public DateTime? MarkAsNewEndDateTimeUtc { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Weight { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Length { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Width { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Height { get; set; }

    [Precision(6)]
    public DateTime? AvailableStartDateTimeUtc { get; set; }

    [Precision(6)]
    public DateTime? AvailableEndDateTimeUtc { get; set; }

    public int DisplayOrder { get; set; }

    public bool Published { get; set; }

    public bool Deleted { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    public bool AgeVerification { get; set; }

    public int MinimumAgeToPurchase { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<BackInStockSubscription> BackInStockSubscriptions { get; set; } = new List<BackInStockSubscription>();

    [InverseProperty("Product")]
    public virtual ICollection<FilterLevelValueProductMapping> FilterLevelValueProductMappings { get; set; } = new List<FilterLevelValueProductMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductAttributeCombination> ProductAttributeCombinations { get; set; } = new List<ProductAttributeCombination>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; } = new List<ProductCategoryMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductManufacturerMapping> ProductManufacturerMappings { get; set; } = new List<ProductManufacturerMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; } = new List<ProductPictureMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductProductAttributeMapping> ProductProductAttributeMappings { get; set; } = new List<ProductProductAttributeMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductSpecificationAttributeMapping> ProductSpecificationAttributeMappings { get; set; } = new List<ProductSpecificationAttributeMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductVideo> ProductVideos { get; set; } = new List<ProductVideo>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductWarehouseInventory> ProductWarehouseInventories { get; set; } = new List<ProductWarehouseInventory>();

    [InverseProperty("Product")]
    public virtual ICollection<RfqquoteItem> RfqquoteItems { get; set; } = new List<RfqquoteItem>();

    [InverseProperty("Product")]
    public virtual ICollection<RfqrequestQuoteItem> RfqrequestQuoteItems { get; set; } = new List<RfqrequestQuoteItem>();

    [InverseProperty("Product")]
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    [InverseProperty("Product")]
    public virtual ICollection<StockQuantityHistory> StockQuantityHistories { get; set; } = new List<StockQuantityHistory>();

    [InverseProperty("Product")]
    public virtual ICollection<TierPrice> TierPrices { get; set; } = new List<TierPrice>();

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
