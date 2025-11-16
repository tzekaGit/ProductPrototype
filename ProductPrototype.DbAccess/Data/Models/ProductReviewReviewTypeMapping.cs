using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductReview_ReviewType_Mapping")]
[Index("ProductReviewId", Name = "IX_ProductReview_ReviewType_Mapping_ProductReviewId")]
[Index("ReviewTypeId", Name = "IX_ProductReview_ReviewType_Mapping_ReviewTypeId")]
public partial class ProductReviewReviewTypeMapping
{
    [Key]
    public int Id { get; set; }

    public int ProductReviewId { get; set; }

    public int ReviewTypeId { get; set; }

    public int Rating { get; set; }

    [ForeignKey("ProductReviewId")]
    [InverseProperty("ProductReviewReviewTypeMappings")]
    public virtual ProductReview ProductReview { get; set; } = null!;

    [ForeignKey("ReviewTypeId")]
    [InverseProperty("ProductReviewReviewTypeMappings")]
    public virtual ReviewType ReviewType { get; set; } = null!;
}
