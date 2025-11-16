using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductAttributeCombinationPicture")]
[Index("ProductAttributeCombinationId", Name = "IX_ProductAttributeCombinationPicture_ProductAttributeCombinationId")]
public partial class ProductAttributeCombinationPicture
{
    [Key]
    public int Id { get; set; }

    public int ProductAttributeCombinationId { get; set; }

    public int PictureId { get; set; }

    [ForeignKey("ProductAttributeCombinationId")]
    [InverseProperty("ProductAttributeCombinationPictures")]
    public virtual ProductAttributeCombination ProductAttributeCombination { get; set; } = null!;
}
