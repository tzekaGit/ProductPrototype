using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ActivityLog")]
[Index("ActivityLogTypeId", Name = "IX_ActivityLog_ActivityLogTypeId")]
[Index("CreatedOnUtc", Name = "IX_ActivityLog_CreatedOnUtc", AllDescending = true)]
[Index("CustomerId", Name = "IX_ActivityLog_CustomerId")]
public partial class ActivityLog
{
    [Key]
    public int Id { get; set; }

    public string Comment { get; set; } = null!;

    [StringLength(100)]
    public string? IpAddress { get; set; }

    [StringLength(400)]
    public string? EntityName { get; set; }

    public int ActivityLogTypeId { get; set; }

    public int CustomerId { get; set; }

    public int? EntityId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("ActivityLogTypeId")]
    [InverseProperty("ActivityLogs")]
    public virtual ActivityLogType ActivityLogType { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("ActivityLogs")]
    public virtual Customer Customer { get; set; } = null!;
}
