using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RFQQuote")]
[Index("CustomerId", Name = "IX_RFQQuote_CustomerId")]
public partial class Rfqquote
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime? ExpirationDateUtc { get; set; }

    public int StatusId { get; set; }

    public int? RequestQuoteId { get; set; }

    public string? AdminNotes { get; set; }

    public int? OrderId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Rfqquotes")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Quote")]
    public virtual ICollection<RfqquoteItem> RfqquoteItems { get; set; } = new List<RfqquoteItem>();
}
