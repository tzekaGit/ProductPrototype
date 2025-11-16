using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ScheduleTask")]
public partial class ScheduleTask
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Seconds { get; set; }

    [Precision(6)]
    public DateTime? LastEnabledUtc { get; set; }

    public bool Enabled { get; set; }

    public bool StopOnError { get; set; }

    [Precision(6)]
    public DateTime? LastStartUtc { get; set; }

    [Precision(6)]
    public DateTime? LastEndUtc { get; set; }

    [Precision(6)]
    public DateTime? LastSuccessUtc { get; set; }
}
