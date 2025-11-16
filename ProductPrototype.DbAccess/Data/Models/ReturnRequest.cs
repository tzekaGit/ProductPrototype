using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ReturnRequest")]
[Index("CustomerId", Name = "IX_ReturnRequest_CustomerId")]
public partial class ReturnRequest
{
    [Key]
    public int Id { get; set; }

    public string ReasonForReturn { get; set; } = null!;

    public string RequestedAction { get; set; } = null!;

    public int CustomerId { get; set; }

    public string? CustomNumber { get; set; }

    public int StoreId { get; set; }

    public int OrderItemId { get; set; }

    public int Quantity { get; set; }

    public int ReturnedQuantity { get; set; }

    public string? CustomerComments { get; set; }

    public int UploadedFileId { get; set; }

    public string? StaffNotes { get; set; }

    public int ReturnRequestStatusId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ReturnRequests")]
    public virtual Customer Customer { get; set; } = null!;
}
