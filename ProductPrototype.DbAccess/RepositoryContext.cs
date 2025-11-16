using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess;

public partial class RepositoryContext : DbContext
{
    public RepositoryContext()
    {
    }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AclRecord> AclRecords { get; set; }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<ActivityLogType> ActivityLogTypes { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressAttribute> AddressAttributes { get; set; }

    public virtual DbSet<AddressAttributeValue> AddressAttributeValues { get; set; }

    public virtual DbSet<Affiliate> Affiliates { get; set; }

    public virtual DbSet<AvalaraItemClassification> AvalaraItemClassifications { get; set; }

    public virtual DbSet<BackInStockSubscription> BackInStockSubscriptions { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryTemplate> CategoryTemplates { get; set; }

    public virtual DbSet<CheckoutAttribute> CheckoutAttributes { get; set; }

    public virtual DbSet<CheckoutAttributeValue> CheckoutAttributeValues { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CrossSellProduct> CrossSellProducts { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CustomWishlist> CustomWishlists { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAttribute> CustomerAttributes { get; set; }

    public virtual DbSet<CustomerAttributeValue> CustomerAttributeValues { get; set; }

    public virtual DbSet<CustomerPassword> CustomerPasswords { get; set; }

    public virtual DbSet<CustomerRole> CustomerRoles { get; set; }

    public virtual DbSet<DeliveryDate> DeliveryDates { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountRequirement> DiscountRequirements { get; set; }

    public virtual DbSet<DiscountUsageHistory> DiscountUsageHistories { get; set; }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<EmailAccount> EmailAccounts { get; set; }

    public virtual DbSet<ExternalAuthenticationRecord> ExternalAuthenticationRecords { get; set; }

    public virtual DbSet<FacebookPixelConfiguration> FacebookPixelConfigurations { get; set; }

    public virtual DbSet<FilterLevelValue> FilterLevelValues { get; set; }

    public virtual DbSet<FilterLevelValueProductMapping> FilterLevelValueProductMappings { get; set; }

    public virtual DbSet<ForumsForum> ForumsForums { get; set; }

    public virtual DbSet<ForumsGroup> ForumsGroups { get; set; }

    public virtual DbSet<ForumsPost> ForumsPosts { get; set; }

    public virtual DbSet<ForumsPostVote> ForumsPostVotes { get; set; }

    public virtual DbSet<ForumsPrivateMessage> ForumsPrivateMessages { get; set; }

    public virtual DbSet<ForumsSubscription> ForumsSubscriptions { get; set; }

    public virtual DbSet<ForumsTopic> ForumsTopics { get; set; }

    public virtual DbSet<GdprConsent> GdprConsents { get; set; }

    public virtual DbSet<GdprLog> GdprLogs { get; set; }

    public virtual DbSet<GenericAttribute> GenericAttributes { get; set; }

    public virtual DbSet<GiftCard> GiftCards { get; set; }

    public virtual DbSet<GiftCardUsageHistory> GiftCardUsageHistories { get; set; }

    public virtual DbSet<GoogleAuthenticatorRecord> GoogleAuthenticatorRecords { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LocaleStringResource> LocaleStringResources { get; set; }

    public virtual DbSet<LocalizedProperty> LocalizedProperties { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<ManufacturerTemplate> ManufacturerTemplates { get; set; }

    public virtual DbSet<MeasureDimension> MeasureDimensions { get; set; }

    public virtual DbSet<MeasureWeight> MeasureWeights { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<MessageTemplate> MessageTemplates { get; set; }

    public virtual DbSet<MigrationVersionInfo> MigrationVersionInfos { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsComment> NewsComments { get; set; }

    public virtual DbSet<NewsLetterSubscription> NewsLetterSubscriptions { get; set; }

    public virtual DbSet<NewsLetterSubscriptionType> NewsLetterSubscriptionTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderNote> OrderNotes { get; set; }

    public virtual DbSet<PayPalToken> PayPalTokens { get; set; }

    public virtual DbSet<PermissionRecord> PermissionRecords { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<PictureBinary> PictureBinaries { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<PollAnswer> PollAnswers { get; set; }

    public virtual DbSet<PollVotingRecord> PollVotingRecords { get; set; }

    public virtual DbSet<PredefinedProductAttributeValue> PredefinedProductAttributeValues { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductAttributeCombination> ProductAttributeCombinations { get; set; }

    public virtual DbSet<ProductAttributeCombinationPicture> ProductAttributeCombinationPictures { get; set; }

    public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

    public virtual DbSet<ProductAttributeValuePicture> ProductAttributeValuePictures { get; set; }

    public virtual DbSet<ProductAvailabilityRange> ProductAvailabilityRanges { get; set; }

    public virtual DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }

    public virtual DbSet<ProductManufacturerMapping> ProductManufacturerMappings { get; set; }

    public virtual DbSet<ProductPictureMapping> ProductPictureMappings { get; set; }

    public virtual DbSet<ProductProductAttributeMapping> ProductProductAttributeMappings { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<ProductReviewHelpfulness> ProductReviewHelpfulnesses { get; set; }

    public virtual DbSet<ProductReviewReviewTypeMapping> ProductReviewReviewTypeMappings { get; set; }

    public virtual DbSet<ProductSpecificationAttributeMapping> ProductSpecificationAttributeMappings { get; set; }

    public virtual DbSet<ProductTag> ProductTags { get; set; }

    public virtual DbSet<ProductTemplate> ProductTemplates { get; set; }

    public virtual DbSet<ProductVideo> ProductVideos { get; set; }

    public virtual DbSet<ProductWarehouseInventory> ProductWarehouseInventories { get; set; }

    public virtual DbSet<QueuedEmail> QueuedEmails { get; set; }

    public virtual DbSet<RecurringPayment> RecurringPayments { get; set; }

    public virtual DbSet<RecurringPaymentHistory> RecurringPaymentHistories { get; set; }

    public virtual DbSet<RelatedProduct> RelatedProducts { get; set; }

    public virtual DbSet<ReturnRequest> ReturnRequests { get; set; }

    public virtual DbSet<ReturnRequestAction> ReturnRequestActions { get; set; }

    public virtual DbSet<ReturnRequestReason> ReturnRequestReasons { get; set; }

    public virtual DbSet<ReviewType> ReviewTypes { get; set; }

    public virtual DbSet<RewardPointsHistory> RewardPointsHistories { get; set; }

    public virtual DbSet<Rfqquote> Rfqquotes { get; set; }

    public virtual DbSet<RfqquoteItem> RfqquoteItems { get; set; }

    public virtual DbSet<RfqrequestQuote> RfqrequestQuotes { get; set; }

    public virtual DbSet<RfqrequestQuoteItem> RfqrequestQuoteItems { get; set; }

    public virtual DbSet<ScheduleTask> ScheduleTasks { get; set; }

    public virtual DbSet<SearchTerm> SearchTerms { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<ShipmentItem> ShipmentItems { get; set; }

    public virtual DbSet<ShippingByWeightByTotalRecord> ShippingByWeightByTotalRecords { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public virtual DbSet<SpecificationAttribute> SpecificationAttributes { get; set; }

    public virtual DbSet<SpecificationAttributeGroup> SpecificationAttributeGroups { get; set; }

    public virtual DbSet<SpecificationAttributeOption> SpecificationAttributeOptions { get; set; }

    public virtual DbSet<StateProvince> StateProvinces { get; set; }

    public virtual DbSet<StockQuantityHistory> StockQuantityHistories { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreMapping> StoreMappings { get; set; }

    public virtual DbSet<StorePickupPoint> StorePickupPoints { get; set; }

    public virtual DbSet<TaxCategory> TaxCategories { get; set; }

    public virtual DbSet<TaxRate> TaxRates { get; set; }

    public virtual DbSet<TaxTransactionLog> TaxTransactionLogs { get; set; }

    public virtual DbSet<TierPrice> TierPrices { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<TopicTemplate> TopicTemplates { get; set; }

    public virtual DbSet<UrlRecord> UrlRecords { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorAttribute> VendorAttributes { get; set; }

    public virtual DbSet<VendorAttributeValue> VendorAttributeValues { get; set; }

    public virtual DbSet<VendorNote> VendorNotes { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<ZettleRecord> ZettleRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=GRAMOS\\SQL2022;Initial Catalog=Belson4.91;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AclRecord>(entity =>
        {
            entity.HasOne(d => d.CustomerRole).WithMany(p => p.AclRecords).HasConstraintName("FK_AclRecord_CustomerRoleId_CustomerRole_Id");
        });

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasOne(d => d.ActivityLogType).WithMany(p => p.ActivityLogs).HasConstraintName("FK_ActivityLog_ActivityLogTypeId_ActivityLogType_Id");

            entity.HasOne(d => d.Customer).WithMany(p => p.ActivityLogs).HasConstraintName("FK_ActivityLog_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne(d => d.Country).WithMany(p => p.Addresses).HasConstraintName("FK_Address_CountryId_Country_Id");

            entity.HasOne(d => d.StateProvince).WithMany(p => p.Addresses).HasConstraintName("FK_Address_StateProvinceId_StateProvince_Id");

            entity.HasMany(d => d.Customers).WithMany(p => p.Addresses)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerAddress",
                    r => r.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_CustomerAddresses_Customer_Id_Customer_Id"),
                    l => l.HasOne<Address>().WithMany()
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_CustomerAddresses_Address_Id_Address_Id"),
                    j =>
                    {
                        j.HasKey("AddressId", "CustomerId");
                        j.ToTable("CustomerAddresses");
                        j.HasIndex(new[] { "AddressId" }, "IX_CustomerAddresses_Address_Id");
                        j.HasIndex(new[] { "CustomerId" }, "IX_CustomerAddresses_Customer_Id");
                        j.IndexerProperty<int>("AddressId").HasColumnName("Address_Id");
                        j.IndexerProperty<int>("CustomerId").HasColumnName("Customer_Id");
                    });
        });

        modelBuilder.Entity<AddressAttributeValue>(entity =>
        {
            entity.HasOne(d => d.AddressAttribute).WithMany(p => p.AddressAttributeValues).HasConstraintName("FK_AddressAttributeValue_AddressAttributeId_AddressAttribute_Id");
        });

        modelBuilder.Entity<Affiliate>(entity =>
        {
            entity.HasOne(d => d.Address).WithMany(p => p.Affiliates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Affiliate_AddressId_Address_Id");
        });

        modelBuilder.Entity<BackInStockSubscription>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.BackInStockSubscriptions).HasConstraintName("FK_BackInStockSubscription_CustomerId_Customer_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.BackInStockSubscriptions).HasConstraintName("FK_BackInStockSubscription_ProductId_Product_Id");
        });

        modelBuilder.Entity<BlogComment>(entity =>
        {
            entity.HasOne(d => d.BlogPost).WithMany(p => p.BlogComments).HasConstraintName("FK_BlogComment_BlogPostId_BlogPost_Id");

            entity.HasOne(d => d.Customer).WithMany(p => p.BlogComments).HasConstraintName("FK_BlogComment_CustomerId_Customer_Id");

            entity.HasOne(d => d.Store).WithMany(p => p.BlogComments).HasConstraintName("FK_BlogComment_StoreId_Store_Id");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasOne(d => d.Language).WithMany(p => p.BlogPosts).HasConstraintName("FK_BlogPost_LanguageId_Language_Id");
        });

        modelBuilder.Entity<CheckoutAttributeValue>(entity =>
        {
            entity.HasOne(d => d.CheckoutAttribute).WithMany(p => p.CheckoutAttributeValues).HasConstraintName("FK_CheckoutAttributeValue_CheckoutAttributeId_CheckoutAttribute_Id");
        });

        modelBuilder.Entity<CustomWishlist>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.CustomWishlists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomWishlist_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(d => d.BillingAddress).WithMany(p => p.CustomerBillingAddresses).HasConstraintName("FK_Customer_BillingAddress_Id_Address_Id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Customer_CurrencyId_Currency_Id");

            entity.HasOne(d => d.Language).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Customer_LanguageId_Language_Id");

            entity.HasOne(d => d.ShippingAddress).WithMany(p => p.CustomerShippingAddresses).HasConstraintName("FK_Customer_ShippingAddress_Id_Address_Id");

            entity.HasMany(d => d.CustomerRoles).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerCustomerRoleMapping",
                    r => r.HasOne<CustomerRole>().WithMany()
                        .HasForeignKey("CustomerRoleId")
                        .HasConstraintName("FK_Customer_CustomerRole_Mapping_CustomerRole_Id_CustomerRole_Id"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Customer_CustomerRole_Mapping_Customer_Id_Customer_Id"),
                    j =>
                    {
                        j.HasKey("CustomerId", "CustomerRoleId");
                        j.ToTable("Customer_CustomerRole_Mapping");
                        j.HasIndex(new[] { "CustomerRoleId" }, "IX_Customer_CustomerRole_Mapping_CustomerRole_Id");
                        j.HasIndex(new[] { "CustomerId" }, "IX_Customer_CustomerRole_Mapping_Customer_Id");
                        j.IndexerProperty<int>("CustomerId").HasColumnName("Customer_Id");
                        j.IndexerProperty<int>("CustomerRoleId").HasColumnName("CustomerRole_Id");
                    });
        });

        modelBuilder.Entity<CustomerAttributeValue>(entity =>
        {
            entity.HasOne(d => d.CustomerAttribute).WithMany(p => p.CustomerAttributeValues).HasConstraintName("FK_CustomerAttributeValue_CustomerAttributeId_CustomerAttribute_Id");
        });

        modelBuilder.Entity<CustomerPassword>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerPasswords).HasConstraintName("FK_CustomerPassword_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasMany(d => d.Categories).WithMany(p => p.Discounts)
                .UsingEntity<Dictionary<string, object>>(
                    "DiscountAppliedToCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Discount_AppliedToCategories_Category_Id_Category_Id"),
                    l => l.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .HasConstraintName("FK_Discount_AppliedToCategories_Discount_Id_Discount_Id"),
                    j =>
                    {
                        j.HasKey("DiscountId", "CategoryId");
                        j.ToTable("Discount_AppliedToCategories");
                        j.HasIndex(new[] { "CategoryId" }, "IX_Discount_AppliedToCategories_Category_Id");
                        j.HasIndex(new[] { "DiscountId" }, "IX_Discount_AppliedToCategories_Discount_Id");
                        j.IndexerProperty<int>("DiscountId").HasColumnName("Discount_Id");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("Category_Id");
                    });

            entity.HasMany(d => d.Manufacturers).WithMany(p => p.Discounts)
                .UsingEntity<Dictionary<string, object>>(
                    "DiscountAppliedToManufacturer",
                    r => r.HasOne<Manufacturer>().WithMany()
                        .HasForeignKey("ManufacturerId")
                        .HasConstraintName("FK_Discount_AppliedToManufacturers_Manufacturer_Id_Manufacturer_Id"),
                    l => l.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .HasConstraintName("FK_Discount_AppliedToManufacturers_Discount_Id_Discount_Id"),
                    j =>
                    {
                        j.HasKey("DiscountId", "ManufacturerId");
                        j.ToTable("Discount_AppliedToManufacturers");
                        j.HasIndex(new[] { "DiscountId" }, "IX_Discount_AppliedToManufacturers_Discount_Id");
                        j.HasIndex(new[] { "ManufacturerId" }, "IX_Discount_AppliedToManufacturers_Manufacturer_Id");
                        j.IndexerProperty<int>("DiscountId").HasColumnName("Discount_Id");
                        j.IndexerProperty<int>("ManufacturerId").HasColumnName("Manufacturer_Id");
                    });

            entity.HasMany(d => d.Products).WithMany(p => p.Discounts)
                .UsingEntity<Dictionary<string, object>>(
                    "DiscountAppliedToProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Discount_AppliedToProducts_Product_Id_Product_Id"),
                    l => l.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .HasConstraintName("FK_Discount_AppliedToProducts_Discount_Id_Discount_Id"),
                    j =>
                    {
                        j.HasKey("DiscountId", "ProductId");
                        j.ToTable("Discount_AppliedToProducts");
                        j.HasIndex(new[] { "DiscountId" }, "IX_Discount_AppliedToProducts_Discount_Id");
                        j.HasIndex(new[] { "ProductId" }, "IX_Discount_AppliedToProducts_Product_Id");
                        j.IndexerProperty<int>("DiscountId").HasColumnName("Discount_Id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("Product_Id");
                    });
        });

        modelBuilder.Entity<DiscountRequirement>(entity =>
        {
            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountRequirements).HasConstraintName("FK_DiscountRequirement_DiscountId_Discount_Id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_DiscountRequirement_ParentId_DiscountRequirement_Id");
        });

        modelBuilder.Entity<DiscountUsageHistory>(entity =>
        {
            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountUsageHistories).HasConstraintName("FK_DiscountUsageHistory_DiscountId_Discount_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.DiscountUsageHistories).HasConstraintName("FK_DiscountUsageHistory_OrderId_Order_Id");
        });

        modelBuilder.Entity<ExternalAuthenticationRecord>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ExternalAuthenticationRecords).HasConstraintName("FK_ExternalAuthenticationRecord_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<FilterLevelValueProductMapping>(entity =>
        {
            entity.HasOne(d => d.FilterLevelValue).WithMany(p => p.FilterLevelValueProductMappings).HasConstraintName("FK_FilterLevelValueProductMapping_FilterLevelValueId_FilterLevelValue_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.FilterLevelValueProductMappings).HasConstraintName("FK_FilterLevelValueProductMapping_ProductId_Product_Id");
        });

        modelBuilder.Entity<ForumsForum>(entity =>
        {
            entity.HasOne(d => d.ForumGroup).WithMany(p => p.ForumsForums).HasConstraintName("FK_Forums_Forum_ForumGroupId_Forums_Group_Id");
        });

        modelBuilder.Entity<ForumsPost>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ForumsPosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Forums_Post_CustomerId_Customer_Id");

            entity.HasOne(d => d.Topic).WithMany(p => p.ForumsPosts).HasConstraintName("FK_Forums_Post_TopicId_Forums_Topic_Id");
        });

        modelBuilder.Entity<ForumsPostVote>(entity =>
        {
            entity.HasOne(d => d.ForumPost).WithMany(p => p.ForumsPostVotes).HasConstraintName("FK_Forums_PostVote_ForumPostId_Forums_Post_Id");
        });

        modelBuilder.Entity<ForumsPrivateMessage>(entity =>
        {
            entity.HasOne(d => d.FromCustomer).WithMany(p => p.ForumsPrivateMessageFromCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Forums_PrivateMessage_FromCustomerId_Customer_Id");

            entity.HasOne(d => d.ToCustomer).WithMany(p => p.ForumsPrivateMessageToCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Forums_PrivateMessage_ToCustomerId_Customer_Id");
        });

        modelBuilder.Entity<ForumsSubscription>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ForumsSubscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Forums_Subscription_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<ForumsTopic>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ForumsTopics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Forums_Topic_CustomerId_Customer_Id");

            entity.HasOne(d => d.Forum).WithMany(p => p.ForumsTopics).HasConstraintName("FK_Forums_Topic_ForumId_Forums_Forum_Id");
        });

        modelBuilder.Entity<GiftCard>(entity =>
        {
            entity.HasOne(d => d.PurchasedWithOrderItem).WithMany(p => p.GiftCards).HasConstraintName("FK_GiftCard_PurchasedWithOrderItemId_OrderItem_Id");
        });

        modelBuilder.Entity<GiftCardUsageHistory>(entity =>
        {
            entity.HasOne(d => d.GiftCard).WithMany(p => p.GiftCardUsageHistories).HasConstraintName("FK_GiftCardUsageHistory_GiftCardId_GiftCard_Id");

            entity.HasOne(d => d.UsedWithOrder).WithMany(p => p.GiftCardUsageHistories).HasConstraintName("FK_GiftCardUsageHistory_UsedWithOrderId_Order_Id");
        });

        modelBuilder.Entity<LocaleStringResource>(entity =>
        {
            entity.HasOne(d => d.Language).WithMany(p => p.LocaleStringResources).HasConstraintName("FK_LocaleStringResource_LanguageId_Language_Id");
        });

        modelBuilder.Entity<LocalizedProperty>(entity =>
        {
            entity.HasOne(d => d.Language).WithMany(p => p.LocalizedProperties).HasConstraintName("FK_LocalizedProperty_LanguageId_Language_Id");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Log_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasOne(d => d.Menu).WithMany(p => p.MenuItems).HasConstraintName("FK_MenuItem_MenuId_Menu_Id");
        });

        modelBuilder.Entity<MigrationVersionInfo>(entity =>
        {
            entity.HasIndex(e => e.Version, "UC_Version")
                .IsUnique()
                .IsClustered();
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasOne(d => d.Language).WithMany(p => p.News).HasConstraintName("FK_News_LanguageId_Language_Id");
        });

        modelBuilder.Entity<NewsComment>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.NewsComments).HasConstraintName("FK_NewsComment_CustomerId_Customer_Id");

            entity.HasOne(d => d.NewsItem).WithMany(p => p.NewsComments).HasConstraintName("FK_NewsComment_NewsItemId_News_Id");

            entity.HasOne(d => d.Store).WithMany(p => p.NewsComments).HasConstraintName("FK_NewsComment_StoreId_Store_Id");
        });

        modelBuilder.Entity<NewsLetterSubscription>(entity =>
        {
            entity.HasOne(d => d.Type).WithMany(p => p.NewsLetterSubscriptions).HasConstraintName("FK_NewsLetterSubscription_TypeId_NewsLetterSubscriptionType_Id");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.BillingAddress).WithMany(p => p.OrderBillingAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_BillingAddressId_Address_Id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_CustomerId_Customer_Id");

            entity.HasOne(d => d.PickupAddress).WithMany(p => p.OrderPickupAddresses).HasConstraintName("FK_Order_PickupAddressId_Address_Id");

            entity.HasOne(d => d.RewardPointsHistoryEntry).WithMany(p => p.Orders).HasConstraintName("FK_Order_RewardPointsHistoryEntryId_RewardPointsHistory_Id");

            entity.HasOne(d => d.ShippingAddress).WithMany(p => p.OrderShippingAddresses).HasConstraintName("FK_Order_ShippingAddressId_Address_Id");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK_OrderItem_OrderId_Order_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems).HasConstraintName("FK_OrderItem_ProductId_Product_Id");
        });

        modelBuilder.Entity<OrderNote>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.OrderNotes).HasConstraintName("FK_OrderNote_OrderId_Order_Id");
        });

        modelBuilder.Entity<PayPalToken>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.PayPalTokens).HasConstraintName("FK_PayPalToken_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<PermissionRecord>(entity =>
        {
            entity.HasMany(d => d.CustomerRoles).WithMany(p => p.PermissionRecords)
                .UsingEntity<Dictionary<string, object>>(
                    "PermissionRecordRoleMapping",
                    r => r.HasOne<CustomerRole>().WithMany()
                        .HasForeignKey("CustomerRoleId")
                        .HasConstraintName("FK_PermissionRecord_Role_Mapping_CustomerRole_Id_CustomerRole_Id"),
                    l => l.HasOne<PermissionRecord>().WithMany()
                        .HasForeignKey("PermissionRecordId")
                        .HasConstraintName("FK_PermissionRecord_Role_Mapping_PermissionRecord_Id_PermissionRecord_Id"),
                    j =>
                    {
                        j.HasKey("PermissionRecordId", "CustomerRoleId");
                        j.ToTable("PermissionRecord_Role_Mapping");
                        j.HasIndex(new[] { "CustomerRoleId" }, "IX_PermissionRecord_Role_Mapping_CustomerRole_Id");
                        j.HasIndex(new[] { "PermissionRecordId" }, "IX_PermissionRecord_Role_Mapping_PermissionRecord_Id");
                        j.IndexerProperty<int>("PermissionRecordId").HasColumnName("PermissionRecord_Id");
                        j.IndexerProperty<int>("CustomerRoleId").HasColumnName("CustomerRole_Id");
                    });
        });

        modelBuilder.Entity<PictureBinary>(entity =>
        {
            entity.HasOne(d => d.Picture).WithMany(p => p.PictureBinaries).HasConstraintName("FK_PictureBinary_PictureId_Picture_Id");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasOne(d => d.Language).WithMany(p => p.Polls).HasConstraintName("FK_Poll_LanguageId_Language_Id");
        });

        modelBuilder.Entity<PollAnswer>(entity =>
        {
            entity.HasOne(d => d.Poll).WithMany(p => p.PollAnswers).HasConstraintName("FK_PollAnswer_PollId_Poll_Id");
        });

        modelBuilder.Entity<PollVotingRecord>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.PollVotingRecords).HasConstraintName("FK_PollVotingRecord_CustomerId_Customer_Id");

            entity.HasOne(d => d.PollAnswer).WithMany(p => p.PollVotingRecords).HasConstraintName("FK_PollVotingRecord_PollAnswerId_PollAnswer_Id");
        });

        modelBuilder.Entity<PredefinedProductAttributeValue>(entity =>
        {
            entity.HasOne(d => d.ProductAttribute).WithMany(p => p.PredefinedProductAttributeValues).HasConstraintName("FK_PredefinedProductAttributeValue_ProductAttributeId_ProductAttribute_Id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasMany(d => d.ProductTags).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductProductTagMapping",
                    r => r.HasOne<ProductTag>().WithMany()
                        .HasForeignKey("ProductTagId")
                        .HasConstraintName("FK_Product_ProductTag_Mapping_ProductTag_Id_ProductTag_Id"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Product_ProductTag_Mapping_Product_Id_Product_Id"),
                    j =>
                    {
                        j.HasKey("ProductId", "ProductTagId");
                        j.ToTable("Product_ProductTag_Mapping");
                        j.HasIndex(new[] { "ProductTagId" }, "IX_Product_ProductTag_Mapping_ProductTag_Id");
                        j.HasIndex(new[] { "ProductId" }, "IX_Product_ProductTag_Mapping_Product_Id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("Product_Id");
                        j.IndexerProperty<int>("ProductTagId").HasColumnName("ProductTag_Id");
                    });
        });

        modelBuilder.Entity<ProductAttributeCombination>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributeCombinations).HasConstraintName("FK_ProductAttributeCombination_ProductId_Product_Id");
        });

        modelBuilder.Entity<ProductAttributeCombinationPicture>(entity =>
        {
            entity.HasOne(d => d.ProductAttributeCombination).WithMany(p => p.ProductAttributeCombinationPictures).HasConstraintName("FK_ProductAttributeCombinationPicture_ProductAttributeCombinationId_ProductAttributeCombination_Id");
        });

        modelBuilder.Entity<ProductAttributeValue>(entity =>
        {
            entity.HasOne(d => d.ProductAttributeMapping).WithMany(p => p.ProductAttributeValues).HasConstraintName("FK_ProductAttributeValue_ProductAttributeMappingId_Product_ProductAttribute_Mapping_Id");
        });

        modelBuilder.Entity<ProductAttributeValuePicture>(entity =>
        {
            entity.HasOne(d => d.ProductAttributeValue).WithMany(p => p.ProductAttributeValuePictures).HasConstraintName("FK_ProductAttributeValuePicture_ProductAttributeValueId_ProductAttributeValue_Id");
        });

        modelBuilder.Entity<ProductCategoryMapping>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategoryMappings).HasConstraintName("FK_Product_Category_Mapping_CategoryId_Category_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategoryMappings).HasConstraintName("FK_Product_Category_Mapping_ProductId_Product_Id");
        });

        modelBuilder.Entity<ProductManufacturerMapping>(entity =>
        {
            entity.HasOne(d => d.Manufacturer).WithMany(p => p.ProductManufacturerMappings).HasConstraintName("FK_Product_Manufacturer_Mapping_ManufacturerId_Manufacturer_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductManufacturerMappings).HasConstraintName("FK_Product_Manufacturer_Mapping_ProductId_Product_Id");
        });

        modelBuilder.Entity<ProductPictureMapping>(entity =>
        {
            entity.HasOne(d => d.Picture).WithMany(p => p.ProductPictureMappings).HasConstraintName("FK_Product_Picture_Mapping_PictureId_Picture_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPictureMappings).HasConstraintName("FK_Product_Picture_Mapping_ProductId_Product_Id");
        });

        modelBuilder.Entity<ProductProductAttributeMapping>(entity =>
        {
            entity.HasOne(d => d.ProductAttribute).WithMany(p => p.ProductProductAttributeMappings).HasConstraintName("FK_Product_ProductAttribute_Mapping_ProductAttributeId_ProductAttribute_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductProductAttributeMappings).HasConstraintName("FK_Product_ProductAttribute_Mapping_ProductId_Product_Id");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ProductReviews).HasConstraintName("FK_ProductReview_CustomerId_Customer_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews).HasConstraintName("FK_ProductReview_ProductId_Product_Id");

            entity.HasOne(d => d.Store).WithMany(p => p.ProductReviews).HasConstraintName("FK_ProductReview_StoreId_Store_Id");
        });

        modelBuilder.Entity<ProductReviewHelpfulness>(entity =>
        {
            entity.HasOne(d => d.ProductReview).WithMany(p => p.ProductReviewHelpfulnesses).HasConstraintName("FK_ProductReviewHelpfulness_ProductReviewId_ProductReview_Id");
        });

        modelBuilder.Entity<ProductReviewReviewTypeMapping>(entity =>
        {
            entity.HasOne(d => d.ProductReview).WithMany(p => p.ProductReviewReviewTypeMappings).HasConstraintName("FK_ProductReview_ReviewType_Mapping_ProductReviewId_ProductReview_Id");

            entity.HasOne(d => d.ReviewType).WithMany(p => p.ProductReviewReviewTypeMappings).HasConstraintName("FK_ProductReview_ReviewType_Mapping_ReviewTypeId_ReviewType_Id");
        });

        modelBuilder.Entity<ProductSpecificationAttributeMapping>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductSpecificationAttributeMappings).HasConstraintName("FK_Product_SpecificationAttribute_Mapping_ProductId_Product_Id");

            entity.HasOne(d => d.SpecificationAttributeOption).WithMany(p => p.ProductSpecificationAttributeMappings).HasConstraintName("FK_Product_SpecificationAttribute_Mapping_SpecificationAttributeOptionId_SpecificationAttributeOption_Id");
        });

        modelBuilder.Entity<ProductVideo>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductVideos).HasConstraintName("FK_ProductVideo_ProductId_Product_Id");

            entity.HasOne(d => d.Video).WithMany(p => p.ProductVideos).HasConstraintName("FK_ProductVideo_VideoId_Video_Id");
        });

        modelBuilder.Entity<ProductWarehouseInventory>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductWarehouseInventories).HasConstraintName("FK_ProductWarehouseInventory_ProductId_Product_Id");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ProductWarehouseInventories).HasConstraintName("FK_ProductWarehouseInventory_WarehouseId_Warehouse_Id");
        });

        modelBuilder.Entity<QueuedEmail>(entity =>
        {
            entity.HasOne(d => d.EmailAccount).WithMany(p => p.QueuedEmails).HasConstraintName("FK_QueuedEmail_EmailAccountId_EmailAccount_Id");
        });

        modelBuilder.Entity<RecurringPayment>(entity =>
        {
            entity.HasOne(d => d.InitialOrder).WithMany(p => p.RecurringPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecurringPayment_InitialOrderId_Order_Id");
        });

        modelBuilder.Entity<RecurringPaymentHistory>(entity =>
        {
            entity.HasOne(d => d.RecurringPayment).WithMany(p => p.RecurringPaymentHistories).HasConstraintName("FK_RecurringPaymentHistory_RecurringPaymentId_RecurringPayment_Id");
        });

        modelBuilder.Entity<ReturnRequest>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ReturnRequests).HasConstraintName("FK_ReturnRequest_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<RewardPointsHistory>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.RewardPointsHistories).HasConstraintName("FK_RewardPointsHistory_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<Rfqquote>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.Rfqquotes).HasConstraintName("FK_RFQQuote_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<RfqquoteItem>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.RfqquoteItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RFQQuoteItem_ProductId_Product_Id");

            entity.HasOne(d => d.Quote).WithMany(p => p.RfqquoteItems).HasConstraintName("FK_RFQQuoteItem_QuoteId_RFQQuote_Id");
        });

        modelBuilder.Entity<RfqrequestQuote>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.RfqrequestQuotes).HasConstraintName("FK_RFQRequestQuote_CustomerId_Customer_Id");
        });

        modelBuilder.Entity<RfqrequestQuoteItem>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.RfqrequestQuoteItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RFQRequestQuoteItem_ProductId_Product_Id");

            entity.HasOne(d => d.RequestQuote).WithMany(p => p.RfqrequestQuoteItems).HasConstraintName("FK_RFQRequestQuoteItem_RequestQuoteId_RFQRequestQuote_Id");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.Shipments).HasConstraintName("FK_Shipment_OrderId_Order_Id");
        });

        modelBuilder.Entity<ShipmentItem>(entity =>
        {
            entity.HasOne(d => d.Shipment).WithMany(p => p.ShipmentItems).HasConstraintName("FK_ShipmentItem_ShipmentId_Shipment_Id");
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasMany(d => d.Countries).WithMany(p => p.ShippingMethods)
                .UsingEntity<Dictionary<string, object>>(
                    "ShippingMethodRestriction",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_ShippingMethodRestrictions_Country_Id_Country_Id"),
                    l => l.HasOne<ShippingMethod>().WithMany()
                        .HasForeignKey("ShippingMethodId")
                        .HasConstraintName("FK_ShippingMethodRestrictions_ShippingMethod_Id_ShippingMethod_Id"),
                    j =>
                    {
                        j.HasKey("ShippingMethodId", "CountryId");
                        j.ToTable("ShippingMethodRestrictions");
                        j.HasIndex(new[] { "CountryId" }, "IX_ShippingMethodRestrictions_Country_Id");
                        j.HasIndex(new[] { "ShippingMethodId" }, "IX_ShippingMethodRestrictions_ShippingMethod_Id");
                        j.IndexerProperty<int>("ShippingMethodId").HasColumnName("ShippingMethod_Id");
                        j.IndexerProperty<int>("CountryId").HasColumnName("Country_Id");
                    });
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.HasOne(d => d.CustomWishlist).WithMany(p => p.ShoppingCartItems)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ShoppingCartItem_CustomWishlistId_CustomWishlist_Id");

            entity.HasOne(d => d.Customer).WithMany(p => p.ShoppingCartItems).HasConstraintName("FK_ShoppingCartItem_CustomerId_Customer_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCartItems).HasConstraintName("FK_ShoppingCartItem_ProductId_Product_Id");
        });

        modelBuilder.Entity<SpecificationAttribute>(entity =>
        {
            entity.HasOne(d => d.SpecificationAttributeGroup).WithMany(p => p.SpecificationAttributes)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SpecificationAttribute_SpecificationAttributeGroupId_SpecificationAttributeGroup_Id");
        });

        modelBuilder.Entity<SpecificationAttributeOption>(entity =>
        {
            entity.HasOne(d => d.SpecificationAttribute).WithMany(p => p.SpecificationAttributeOptions).HasConstraintName("FK_SpecificationAttributeOption_SpecificationAttributeId_SpecificationAttribute_Id");
        });

        modelBuilder.Entity<StateProvince>(entity =>
        {
            entity.HasOne(d => d.Country).WithMany(p => p.StateProvinces).HasConstraintName("FK_StateProvince_CountryId_Country_Id");
        });

        modelBuilder.Entity<StockQuantityHistory>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.StockQuantityHistories).HasConstraintName("FK_StockQuantityHistory_ProductId_Product_Id");
        });

        modelBuilder.Entity<StoreMapping>(entity =>
        {
            entity.HasOne(d => d.Store).WithMany(p => p.StoreMappings).HasConstraintName("FK_StoreMapping_StoreId_Store_Id");
        });

        modelBuilder.Entity<TierPrice>(entity =>
        {
            entity.HasOne(d => d.CustomerRole).WithMany(p => p.TierPrices)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TierPrice_CustomerRoleId_CustomerRole_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.TierPrices).HasConstraintName("FK_TierPrice_ProductId_Product_Id");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasOne(d => d.PmCustomer).WithMany(p => p.Vendors)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Vendor_PmCustomerId_Customer_Id");
        });

        modelBuilder.Entity<VendorAttributeValue>(entity =>
        {
            entity.HasOne(d => d.VendorAttribute).WithMany(p => p.VendorAttributeValues).HasConstraintName("FK_VendorAttributeValue_VendorAttributeId_VendorAttribute_Id");
        });

        modelBuilder.Entity<VendorNote>(entity =>
        {
            entity.HasOne(d => d.Vendor).WithMany(p => p.VendorNotes).HasConstraintName("FK_VendorNote_VendorId_Vendor_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
