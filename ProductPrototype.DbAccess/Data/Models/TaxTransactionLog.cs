using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("TaxTransactionLog")]
public partial class TaxTransactionLog
{
    [Key]
    public int Id { get; set; }

    public int StatusCode { get; set; }

    public string? Url { get; set; }

    public string? RequestMessage { get; set; }

    public string? ResponseMessage { get; set; }

    public int CustomerId { get; set; }

    [Precision(6)]
    public DateTime CreatedDateUtc { get; set; }
}
