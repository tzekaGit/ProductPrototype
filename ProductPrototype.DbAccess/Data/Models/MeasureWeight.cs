using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("MeasureWeight")]
public partial class MeasureWeight
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string SystemKeyword { get; set; } = null!;

    [Column(TypeName = "decimal(18, 8)")]
    public decimal Ratio { get; set; }

    public int DisplayOrder { get; set; }
}
