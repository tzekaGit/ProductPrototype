using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Discount")]
public partial class Discount
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? CouponCode { get; set; }

    public string? AdminComment { get; set; }

    public int DiscountTypeId { get; set; }

    public bool UsePercentage { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal DiscountPercentage { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? MaximumDiscountAmount { get; set; }

    [Precision(6)]
    public DateTime? StartDateUtc { get; set; }

    [Precision(6)]
    public DateTime? EndDateUtc { get; set; }

    public bool RequiresCouponCode { get; set; }

    public bool IsCumulative { get; set; }

    public int DiscountLimitationId { get; set; }

    public int LimitationTimes { get; set; }

    public int? MaximumDiscountedQuantity { get; set; }

    public bool AppliedToSubCategories { get; set; }

    public bool IsActive { get; set; }

    public int? VendorId { get; set; }

    [InverseProperty("Discount")]
    public virtual ICollection<DiscountRequirement> DiscountRequirements { get; set; } = new List<DiscountRequirement>();

    [InverseProperty("Discount")]
    public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; } = new List<DiscountUsageHistory>();

    [ForeignKey("DiscountId")]
    [InverseProperty("Discounts")]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    [ForeignKey("DiscountId")]
    [InverseProperty("Discounts")]
    public virtual ICollection<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();

    [ForeignKey("DiscountId")]
    [InverseProperty("Discounts")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
