using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("PictureBinary")]
[Index("PictureId", Name = "IX_PictureBinary_PictureId")]
public partial class PictureBinary
{
    [Key]
    public int Id { get; set; }

    public int PictureId { get; set; }

    public byte[]? BinaryData { get; set; }

    [ForeignKey("PictureId")]
    [InverseProperty("PictureBinaries")]
    public virtual Picture Picture { get; set; } = null!;
}
