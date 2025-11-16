using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ZettleRecord")]
public partial class ZettleRecord
{
    [Key]
    public int Id { get; set; }

    public bool Active { get; set; }

    public string? Uuid { get; set; }

    public int ProductId { get; set; }

    public string? VariantUuid { get; set; }

    public int CombinationId { get; set; }

    public string? ImageUrl { get; set; }

    public int OperationTypeId { get; set; }

    public bool PriceSyncEnabled { get; set; }

    public bool ImageSyncEnabled { get; set; }

    public bool InventoryTrackingEnabled { get; set; }

    public string? ExternalUuid { get; set; }

    [Precision(6)]
    public DateTime? UpdatedOnUtc { get; set; }
}
