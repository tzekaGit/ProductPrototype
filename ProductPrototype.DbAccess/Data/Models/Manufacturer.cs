using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Manufacturer")]
[Index("DisplayOrder", Name = "IX_Manufacturer_DisplayOrder")]
[Index("LimitedToStores", Name = "IX_Manufacturer_LimitedToStores")]
[Index("Name", "Deleted", Name = "IX_Manufacturer_Name_Deleted")]
[Index("SubjectToAcl", Name = "IX_Manufacturer_SubjectToAcl")]
public partial class Manufacturer
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string? MetaKeywords { get; set; }

    [StringLength(400)]
    public string? MetaTitle { get; set; }

    [StringLength(200)]
    public string? PageSizeOptions { get; set; }

    public string? Description { get; set; }

    public int ManufacturerTemplateId { get; set; }

    public string? MetaDescription { get; set; }

    public int PictureId { get; set; }

    public int PageSize { get; set; }

    public bool AllowCustomersToSelectPageSize { get; set; }

    public bool SubjectToAcl { get; set; }

    public bool LimitedToStores { get; set; }

    public bool Published { get; set; }

    public bool Deleted { get; set; }

    public int DisplayOrder { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    public bool PriceRangeFiltering { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceFrom { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PriceTo { get; set; }

    public bool ManuallyPriceRange { get; set; }

    [InverseProperty("Manufacturer")]
    public virtual ICollection<ProductManufacturerMapping> ProductManufacturerMappings { get; set; } = new List<ProductManufacturerMapping>();

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Manufacturers")]
    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
