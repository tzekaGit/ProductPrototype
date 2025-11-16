using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Index("LanguageId", Name = "IX_News_LanguageId")]
public partial class News
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Short { get; set; } = null!;

    public string Full { get; set; } = null!;

    [StringLength(400)]
    public string? MetaKeywords { get; set; }

    [StringLength(400)]
    public string? MetaTitle { get; set; }

    public int LanguageId { get; set; }

    public bool Published { get; set; }

    [Precision(6)]
    public DateTime? StartDateUtc { get; set; }

    [Precision(6)]
    public DateTime? EndDateUtc { get; set; }

    public bool AllowComments { get; set; }

    public bool LimitedToStores { get; set; }

    public string? MetaDescription { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("News")]
    public virtual Language Language { get; set; } = null!;

    [InverseProperty("NewsItem")]
    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();
}
