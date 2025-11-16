using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Currency")]
[Index("DisplayOrder", Name = "IX_Currency_DisplayOrder")]
public partial class Currency
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(5)]
    public string CurrencyCode { get; set; } = null!;

    [StringLength(50)]
    public string? DisplayLocale { get; set; }

    [StringLength(50)]
    public string? CustomFormatting { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Rate { get; set; }

    public bool LimitedToStores { get; set; }

    public bool Published { get; set; }

    public int DisplayOrder { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    public int RoundingTypeId { get; set; }

    [InverseProperty("Currency")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
