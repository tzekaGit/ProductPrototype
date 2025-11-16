using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ActivityLogType")]
public partial class ActivityLogType
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string SystemKeyword { get; set; } = null!;

    [StringLength(200)]
    public string Name { get; set; } = null!;

    public bool Enabled { get; set; }

    [InverseProperty("ActivityLogType")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
}
