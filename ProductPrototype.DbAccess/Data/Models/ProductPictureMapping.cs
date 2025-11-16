using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Product_Picture_Mapping")]
[Index("PictureId", Name = "IX_Product_Picture_Mapping_PictureId")]
[Index("ProductId", Name = "IX_Product_Picture_Mapping_ProductId")]
public partial class ProductPictureMapping
{
    [Key]
    public int Id { get; set; }

    public int PictureId { get; set; }

    public int ProductId { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("PictureId")]
    [InverseProperty("ProductPictureMappings")]
    public virtual Picture Picture { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductPictureMappings")]
    public virtual Product Product { get; set; } = null!;
}
