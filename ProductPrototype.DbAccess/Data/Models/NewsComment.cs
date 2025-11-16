using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("NewsComment")]
[Index("CustomerId", Name = "IX_NewsComment_CustomerId")]
[Index("NewsItemId", Name = "IX_NewsComment_NewsItemId")]
[Index("StoreId", Name = "IX_NewsComment_StoreId")]
public partial class NewsComment
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int NewsItemId { get; set; }

    public int StoreId { get; set; }

    public string? CommentTitle { get; set; }

    public string? CommentText { get; set; }

    public bool IsApproved { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("NewsComments")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("NewsItemId")]
    [InverseProperty("NewsComments")]
    public virtual News NewsItem { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("NewsComments")]
    public virtual Store Store { get; set; } = null!;
}
