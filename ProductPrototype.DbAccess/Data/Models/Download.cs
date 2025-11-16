using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Download")]
public partial class Download
{
    [Key]
    public int Id { get; set; }

    public Guid DownloadGuid { get; set; }

    public bool UseDownloadUrl { get; set; }

    public string? DownloadUrl { get; set; }

    public byte[]? DownloadBinary { get; set; }

    public string? ContentType { get; set; }

    public string? Filename { get; set; }

    public string? Extension { get; set; }

    public bool IsNew { get; set; }
}
