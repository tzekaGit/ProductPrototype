using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("BlogPost")]
[Index("LanguageId", Name = "IX_BlogPost_LanguageId")]
public partial class BlogPost
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    [StringLength(400)]
    public string? MetaKeywords { get; set; }

    [StringLength(400)]
    public string? MetaTitle { get; set; }

    public int LanguageId { get; set; }

    public bool IncludeInSitemap { get; set; }

    public string? BodyOverview { get; set; }

    public bool AllowComments { get; set; }

    public string? Tags { get; set; }

    [Precision(6)]
    public DateTime? StartDateUtc { get; set; }

    [Precision(6)]
    public DateTime? EndDateUtc { get; set; }

    public string? MetaDescription { get; set; }

    public bool LimitedToStores { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [InverseProperty("BlogPost")]
    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    [ForeignKey("LanguageId")]
    [InverseProperty("BlogPosts")]
    public virtual Language Language { get; set; } = null!;
}
