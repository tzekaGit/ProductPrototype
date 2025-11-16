using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductTag")]
[Index("Name", Name = "IX_ProductTag_Name")]
public partial class ProductTag
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string? MetaKeywords { get; set; }

    [StringLength(400)]
    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    [ForeignKey("ProductTagId")]
    [InverseProperty("ProductTags")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
