using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RFQRequestQuoteItem")]
[Index("ProductId", Name = "IX_RFQRequestQuoteItem_ProductId")]
[Index("RequestQuoteId", Name = "IX_RFQRequestQuoteItem_RequestQuoteId")]
public partial class RfqrequestQuoteItem
{
    [Key]
    public int Id { get; set; }

    public int RequestQuoteId { get; set; }

    public int ProductId { get; set; }

    public string? ProductAttributesXml { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OriginalProductPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal RequestedUnitPrice { get; set; }

    public int RequestedQty { get; set; }

    public string? AdminNotes { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("RfqrequestQuoteItems")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("RequestQuoteId")]
    [InverseProperty("RfqrequestQuoteItems")]
    public virtual RfqrequestQuote RequestQuote { get; set; } = null!;
}
