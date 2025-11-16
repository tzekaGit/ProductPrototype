using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Vendor")]
[Index("PmCustomerId", Name = "IX_Vendor_PmCustomerId")]
public partial class Vendor
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string? Email { get; set; }

    [StringLength(400)]
    public string? MetaKeywords { get; set; }

    [StringLength(400)]
    public string? MetaTitle { get; set; }

    [StringLength(200)]
    public string? PageSizeOptions { get; set; }

    public int? PmCustomerId { get; set; }

    public string? Description { get; set; }

    public int PictureId { get; set; }

    public int AddressId { get; set; }

    public string? AdminComment { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public int DisplayOrder { get; set; }

    public string? MetaDescription { get; set; }

    public int PageSize { get; set; }

    public bool AllowCustomersToSelectPageSize { get; set; }

    public bool PriceRangeFiltering { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceFrom { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceTo { get; set; }

    public bool ManuallyPriceRange { get; set; }

    [ForeignKey("PmCustomerId")]
    [InverseProperty("Vendors")]
    public virtual Customer? PmCustomer { get; set; }

    [InverseProperty("Vendor")]
    public virtual ICollection<VendorNote> VendorNotes { get; set; } = new List<VendorNote>();
}
