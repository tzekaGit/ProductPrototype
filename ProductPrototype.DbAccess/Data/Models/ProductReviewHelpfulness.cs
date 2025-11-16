using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductReviewHelpfulness")]
[Index("ProductReviewId", Name = "IX_ProductReviewHelpfulness_ProductReviewId")]
public partial class ProductReviewHelpfulness
{
    [Key]
    public int Id { get; set; }

    public int ProductReviewId { get; set; }

    public bool WasHelpful { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("ProductReviewId")]
    [InverseProperty("ProductReviewHelpfulnesses")]
    public virtual ProductReview ProductReview { get; set; } = null!;
}
