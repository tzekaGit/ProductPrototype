using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Product_Category_Mapping")]
[Index("ProductId", "IsFeaturedProduct", Name = "IX_PCM_ProductId_Extended")]
[Index("CategoryId", "ProductId", Name = "IX_PCM_Product_and_Category")]
[Index("CategoryId", Name = "IX_ProductCategoryMapping_CategoryId")]
[Index("CategoryId", Name = "IX_Product_Category_Mapping_CategoryId")]
[Index("IsFeaturedProduct", Name = "IX_Product_Category_Mapping_IsFeaturedProduct")]
[Index("ProductId", Name = "IX_Product_Category_Mapping_ProductId")]
public partial class ProductCategoryMapping
{
    [Key]
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int ProductId { get; set; }

    public bool IsFeaturedProduct { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("ProductCategoryMappings")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductCategoryMappings")]
    public virtual Product Product { get; set; } = null!;
}
