using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("OrderNote")]
[Index("OrderId", Name = "IX_OrderNote_OrderId")]
public partial class OrderNote
{
    [Key]
    public int Id { get; set; }

    public string Note { get; set; } = null!;

    public int OrderId { get; set; }

    public int DownloadId { get; set; }

    public bool DisplayToCustomer { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderNotes")]
    public virtual Order Order { get; set; } = null!;
}
