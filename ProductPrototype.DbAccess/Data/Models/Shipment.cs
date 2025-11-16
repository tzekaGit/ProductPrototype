using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Shipment")]
[Index("OrderId", Name = "IX_Shipment_OrderId")]
public partial class Shipment
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? TrackingNumber { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? TotalWeight { get; set; }

    [Precision(6)]
    public DateTime? ShippedDateUtc { get; set; }

    [Precision(6)]
    public DateTime? DeliveryDateUtc { get; set; }

    [Precision(6)]
    public DateTime? ReadyForPickupDateUtc { get; set; }

    public string? AdminComment { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Shipments")]
    public virtual Order Order { get; set; } = null!;

    [InverseProperty("Shipment")]
    public virtual ICollection<ShipmentItem> ShipmentItems { get; set; } = new List<ShipmentItem>();
}
