using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RFQQuoteItem")]
[Index("ProductId", Name = "IX_RFQQuoteItem_ProductId")]
[Index("QuoteId", Name = "IX_RFQQuoteItem_QuoteId")]
public partial class RfqquoteItem
{
    [Key]
    public int Id { get; set; }

    public int QuoteId { get; set; }

    public int ProductId { get; set; }

    public int? RequestedQty { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? RequestedUnitPrice { get; set; }

    public int OfferedQty { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OfferedUnitPrice { get; set; }

    public string? AttributesXml { get; set; }

    public int? RequestQuoteId { get; set; }

    public int? ShoppingCartItemId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("RfqquoteItems")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("QuoteId")]
    [InverseProperty("RfqquoteItems")]
    public virtual Rfqquote Quote { get; set; } = null!;
}
