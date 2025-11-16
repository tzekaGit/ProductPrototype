using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("StorePickupPoint")]
public partial class StorePickupPoint
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? Longitude { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int AddressId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal PickupFee { get; set; }

    public string? OpeningHours { get; set; }

    public int DisplayOrder { get; set; }

    public int StoreId { get; set; }

    public int? TransitDays { get; set; }
}
