using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Warehouse")]
public partial class Warehouse
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public string? AdminComment { get; set; }

    public int AddressId { get; set; }

    [InverseProperty("Warehouse")]
    public virtual ICollection<ProductWarehouseInventory> ProductWarehouseInventories { get; set; } = new List<ProductWarehouseInventory>();
}
