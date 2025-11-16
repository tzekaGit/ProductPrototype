using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductReview")]
[Index("CustomerId", Name = "IX_ProductReview_CustomerId")]
[Index("ProductId", Name = "IX_ProductReview_ProductId")]
[Index("StoreId", Name = "IX_ProductReview_StoreId")]
public partial class ProductReview
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int StoreId { get; set; }

    public bool IsApproved { get; set; }

    public string? Title { get; set; }

    public string? ReviewText { get; set; }

    public string? ReplyText { get; set; }

    public bool CustomerNotifiedOfReply { get; set; }

    public int Rating { get; set; }

    public int HelpfulYesTotal { get; set; }

    public int HelpfulNoTotal { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ProductReviews")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductReviews")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("ProductReview")]
    public virtual ICollection<ProductReviewHelpfulness> ProductReviewHelpfulnesses { get; set; } = new List<ProductReviewHelpfulness>();

    [InverseProperty("ProductReview")]
    public virtual ICollection<ProductReviewReviewTypeMapping> ProductReviewReviewTypeMappings { get; set; } = new List<ProductReviewReviewTypeMapping>();

    [ForeignKey("StoreId")]
    [InverseProperty("ProductReviews")]
    public virtual Store Store { get; set; } = null!;
}
