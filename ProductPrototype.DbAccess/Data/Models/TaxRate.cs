using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("TaxRate")]
public partial class TaxRate
{
    [Key]
    public int Id { get; set; }

    public int StoreId { get; set; }

    public int TaxCategoryId { get; set; }

    public int CountryId { get; set; }

    public int StateProvinceId { get; set; }

    public string? Zip { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Percentage { get; set; }
}
