using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Video")]
public partial class Video
{
    [Key]
    public int Id { get; set; }

    [StringLength(1000)]
    public string VideoUrl { get; set; } = null!;

    [InverseProperty("Video")]
    public virtual ICollection<ProductVideo> ProductVideos { get; set; } = new List<ProductVideo>();
}
