using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("FilterLevelValueProductMapping")]
[Index("FilterLevelValueId", Name = "IX_FilterLevelValueProductMapping_FilterLevelValueId")]
[Index("ProductId", Name = "IX_FilterLevelValueProductMapping_ProductId")]
public partial class FilterLevelValueProductMapping
{
    [Key]
    public int Id { get; set; }

    public int FilterLevelValueId { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("FilterLevelValueId")]
    [InverseProperty("FilterLevelValueProductMappings")]
    public virtual FilterLevelValue FilterLevelValue { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("FilterLevelValueProductMappings")]
    public virtual Product Product { get; set; } = null!;
}
