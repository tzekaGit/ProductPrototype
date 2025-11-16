using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ShippingByWeightByTotalRecord")]
public partial class ShippingByWeightByTotalRecord
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal WeightFrom { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal WeightTo { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderSubtotalFrom { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal OrderSubtotalTo { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal AdditionalFixedCost { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PercentageRateOfSubtotal { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal RatePerWeightUnit { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal LowerWeightLimit { get; set; }

    [StringLength(400)]
    public string? Zip { get; set; }

    public int StoreId { get; set; }

    public int WarehouseId { get; set; }

    public int CountryId { get; set; }

    public int StateProvinceId { get; set; }

    public int ShippingMethodId { get; set; }

    public int? TransitDays { get; set; }
}
