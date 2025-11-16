using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("AvalaraItemClassification")]
public partial class AvalaraItemClassification
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Column("HSClassificationRequestId")]
    public string? HsclassificationRequestId { get; set; }

    public int CountryId { get; set; }

    [Column("HSCode")]
    public string? Hscode { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }
}
