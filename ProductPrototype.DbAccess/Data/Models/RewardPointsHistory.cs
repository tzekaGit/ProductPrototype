using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RewardPointsHistory")]
[Index("CustomerId", Name = "IX_RewardPointsHistory_CustomerId")]
public partial class RewardPointsHistory
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int StoreId { get; set; }

    public int Points { get; set; }

    public int? PointsBalance { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal UsedAmount { get; set; }

    public string? Message { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime? EndDateUtc { get; set; }

    public int? ValidPoints { get; set; }

    public Guid? UsedWithOrder { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RewardPointsHistories")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("RewardPointsHistoryEntry")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
