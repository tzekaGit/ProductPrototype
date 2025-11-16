using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Category")]
[Index("Deleted", Name = "IX_Category_Deleted_Extended")]
[Index("DisplayOrder", Name = "IX_Category_DisplayOrder")]
[Index("LimitedToStores", Name = "IX_Category_LimitedToStores")]
[Index("Name", "Deleted", Name = "IX_Category_Name_Deleted")]
[Index("ParentCategoryId", Name = "IX_Category_ParentCategoryId")]
[Index("SubjectToAcl", Name = "IX_Category_SubjectToAcl")]
public partial class Category
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

    public int CategoryTemplateId { get; set; }

    public string? MetaDescription { get; set; }

    public int ParentCategoryId { get; set; }

    public int PictureId { get; set; }

    public int PageSize { get; set; }

    public bool AllowCustomersToSelectPageSize { get; set; }

    public bool ShowOnHomepage { get; set; }

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

    public bool RestrictFromVendors { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; } = new List<ProductCategoryMapping>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Categories")]
    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
