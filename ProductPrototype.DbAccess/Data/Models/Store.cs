using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Store")]
public partial class Store
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string Url { get; set; } = null!;

    [StringLength(1000)]
    public string? Hosts { get; set; }

    [StringLength(1000)]
    public string? CompanyName { get; set; }

    [StringLength(1000)]
    public string? CompanyAddress { get; set; }

    [StringLength(1000)]
    public string? CompanyPhoneNumber { get; set; }

    [StringLength(1000)]
    public string? CompanyVat { get; set; }

    public string? DefaultMetaKeywords { get; set; }

    public string? DefaultMetaDescription { get; set; }

    public string? DefaultTitle { get; set; }

    public string? HomepageTitle { get; set; }

    public string? HomepageDescription { get; set; }

    public bool SslEnabled { get; set; }

    public int DefaultLanguageId { get; set; }

    public int DisplayOrder { get; set; }

    public bool Deleted { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    [InverseProperty("Store")]
    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    [InverseProperty("Store")]
    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    [InverseProperty("Store")]
    public virtual ICollection<StoreMapping> StoreMappings { get; set; } = new List<StoreMapping>();
}
