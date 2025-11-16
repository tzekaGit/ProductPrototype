using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ReviewType")]
public partial class ReviewType
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string Description { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool VisibleToAllCustomers { get; set; }

    public bool IsRequired { get; set; }

    [InverseProperty("ReviewType")]
    public virtual ICollection<ProductReviewReviewTypeMapping> ProductReviewReviewTypeMappings { get; set; } = new List<ProductReviewReviewTypeMapping>();
}
