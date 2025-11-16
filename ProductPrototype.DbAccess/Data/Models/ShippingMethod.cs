using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ShippingMethod")]
public partial class ShippingMethod
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("ShippingMethodId")]
    [InverseProperty("ShippingMethods")]
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
