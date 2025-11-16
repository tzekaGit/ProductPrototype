using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RecurringPayment")]
[Index("InitialOrderId", Name = "IX_RecurringPayment_InitialOrderId")]
public partial class RecurringPayment
{
    [Key]
    public int Id { get; set; }

    public int InitialOrderId { get; set; }

    public int CycleLength { get; set; }

    public int CyclePeriodId { get; set; }

    public int TotalCycles { get; set; }

    [Precision(6)]
    public DateTime StartDateUtc { get; set; }

    public bool IsActive { get; set; }

    public bool LastPaymentFailed { get; set; }

    public bool Deleted { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("InitialOrderId")]
    [InverseProperty("RecurringPayments")]
    public virtual Order InitialOrder { get; set; } = null!;

    [InverseProperty("RecurringPayment")]
    public virtual ICollection<RecurringPaymentHistory> RecurringPaymentHistories { get; set; } = new List<RecurringPaymentHistory>();
}
