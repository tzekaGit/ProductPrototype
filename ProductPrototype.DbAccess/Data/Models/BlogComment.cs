using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("BlogComment")]
[Index("BlogPostId", Name = "IX_BlogComment_BlogPostId")]
[Index("CustomerId", Name = "IX_BlogComment_CustomerId")]
[Index("StoreId", Name = "IX_BlogComment_StoreId")]
public partial class BlogComment
{
    [Key]
    public int Id { get; set; }

    public int StoreId { get; set; }

    public int CustomerId { get; set; }

    public int BlogPostId { get; set; }

    public string? CommentText { get; set; }

    public bool IsApproved { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("BlogPostId")]
    [InverseProperty("BlogComments")]
    public virtual BlogPost BlogPost { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("BlogComments")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("BlogComments")]
    public virtual Store Store { get; set; } = null!;
}
