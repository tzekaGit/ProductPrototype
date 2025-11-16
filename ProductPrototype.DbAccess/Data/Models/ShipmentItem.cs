using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ShipmentItem")]
[Index("ShipmentId", Name = "IX_ShipmentItem_ShipmentId")]
public partial class ShipmentItem
{
    [Key]
    public int Id { get; set; }

    public int ShipmentId { get; set; }

    public int OrderItemId { get; set; }

    public int Quantity { get; set; }

    public int WarehouseId { get; set; }

    [ForeignKey("ShipmentId")]
    [InverseProperty("ShipmentItems")]
    public virtual Shipment Shipment { get; set; } = null!;
}
