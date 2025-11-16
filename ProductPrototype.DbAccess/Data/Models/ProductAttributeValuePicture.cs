using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductAttributeValuePicture")]
[Index("ProductAttributeValueId", Name = "IX_ProductAttributeValuePicture_ProductAttributeValueId")]
public partial class ProductAttributeValuePicture
{
    [Key]
    public int Id { get; set; }

    public int ProductAttributeValueId { get; set; }

    public int PictureId { get; set; }

    [ForeignKey("ProductAttributeValueId")]
    [InverseProperty("ProductAttributeValuePictures")]
    public virtual ProductAttributeValue ProductAttributeValue { get; set; } = null!;
}
