using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("FilterLevelValue")]
public partial class FilterLevelValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? FilterLevel1Value { get; set; }

    [StringLength(100)]
    public string? FilterLevel2Value { get; set; }

    [StringLength(100)]
    public string? FilterLevel3Value { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    [InverseProperty("FilterLevelValue")]
    public virtual ICollection<FilterLevelValueProductMapping> FilterLevelValueProductMappings { get; set; } = new List<FilterLevelValueProductMapping>();
}
