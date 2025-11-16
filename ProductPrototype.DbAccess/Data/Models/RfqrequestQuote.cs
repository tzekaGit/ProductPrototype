using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RFQRequestQuote")]
[Index("CustomerId", Name = "IX_RFQRequestQuote_CustomerId")]
public partial class RfqrequestQuote
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    public int StatusId { get; set; }

    public int? QuoteId { get; set; }

    public string? CustomerNotes { get; set; }

    public string? AdminNotes { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RfqrequestQuotes")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("RequestQuote")]
    public virtual ICollection<RfqrequestQuoteItem> RfqrequestQuoteItems { get; set; } = new List<RfqrequestQuoteItem>();
}
