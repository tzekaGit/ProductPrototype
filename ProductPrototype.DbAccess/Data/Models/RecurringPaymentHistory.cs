using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RecurringPaymentHistory")]
[Index("RecurringPaymentId", Name = "IX_RecurringPaymentHistory_RecurringPaymentId")]
public partial class RecurringPaymentHistory
{
    [Key]
    public int Id { get; set; }

    public int RecurringPaymentId { get; set; }

    public int OrderId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("RecurringPaymentId")]
    [InverseProperty("RecurringPaymentHistories")]
    public virtual RecurringPayment RecurringPayment { get; set; } = null!;
}
