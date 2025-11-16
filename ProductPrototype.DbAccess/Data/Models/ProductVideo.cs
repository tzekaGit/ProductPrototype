using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ProductVideo")]
[Index("ProductId", Name = "IX_ProductVideo_ProductId")]
[Index("VideoId", Name = "IX_ProductVideo_VideoId")]
public partial class ProductVideo
{
    [Key]
    public int Id { get; set; }

    public int VideoId { get; set; }

    public int ProductId { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductVideos")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("VideoId")]
    [InverseProperty("ProductVideos")]
    public virtual Video Video { get; set; } = null!;
}
