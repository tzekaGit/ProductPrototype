using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Language")]
[Index("DisplayOrder", Name = "IX_Language_DisplayOrder")]
public partial class Language
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string LanguageCulture { get; set; } = null!;

    [StringLength(2)]
    public string? UniqueSeoCode { get; set; }

    [StringLength(50)]
    public string? FlagImageFileName { get; set; }

    public bool Rtl { get; set; }

    public bool LimitedToStores { get; set; }

    public int DefaultCurrencyId { get; set; }

    public bool Published { get; set; }

    public int DisplayOrder { get; set; }

    [InverseProperty("Language")]
    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    [InverseProperty("Language")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Language")]
    public virtual ICollection<LocaleStringResource> LocaleStringResources { get; set; } = new List<LocaleStringResource>();

    [InverseProperty("Language")]
    public virtual ICollection<LocalizedProperty> LocalizedProperties { get; set; } = new List<LocalizedProperty>();

    [InverseProperty("Language")]
    public virtual ICollection<News> News { get; set; } = new List<News>();

    [InverseProperty("Language")]
    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();
}
