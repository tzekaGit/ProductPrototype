using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Picture")]
public partial class Picture
{
    [Key]
    public int Id { get; set; }

    [StringLength(40)]
    public string MimeType { get; set; } = null!;

    [StringLength(300)]
    public string? SeoFilename { get; set; }

    public string? AltAttribute { get; set; }

    public string? TitleAttribute { get; set; }

    public bool IsNew { get; set; }

    public string? VirtualPath { get; set; }

    [InverseProperty("Picture")]
    public virtual ICollection<PictureBinary> PictureBinaries { get; set; } = new List<PictureBinary>();

    [InverseProperty("Picture")]
    public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; } = new List<ProductPictureMapping>();
}
