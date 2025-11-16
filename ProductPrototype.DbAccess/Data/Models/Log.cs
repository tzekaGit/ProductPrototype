using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Log")]
[Index("CreatedOnUtc", Name = "IX_Log_CreatedOnUtc", AllDescending = true)]
[Index("CustomerId", Name = "IX_Log_CustomerId")]
public partial class Log
{
    [Key]
    public int Id { get; set; }

    public string ShortMessage { get; set; } = null!;

    [StringLength(100)]
    public string? IpAddress { get; set; }

    public int? CustomerId { get; set; }

    public int LogLevelId { get; set; }

    public string? FullMessage { get; set; }

    public string? PageUrl { get; set; }

    public string? ReferrerUrl { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Logs")]
    public virtual Customer? Customer { get; set; }
}
