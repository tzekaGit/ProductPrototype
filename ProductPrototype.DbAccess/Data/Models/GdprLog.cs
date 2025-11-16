using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("GdprLog")]
public partial class GdprLog
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ConsentId { get; set; }

    public string? CustomerInfo { get; set; }

    public int RequestTypeId { get; set; }

    public string? RequestDetails { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }
}
