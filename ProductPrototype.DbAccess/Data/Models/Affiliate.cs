using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Affiliate")]
[Index("AddressId", Name = "IX_Affiliate_AddressId")]
public partial class Affiliate
{
    [Key]
    public int Id { get; set; }

    public int AddressId { get; set; }

    public string? AdminComment { get; set; }

    public string? FriendlyUrlName { get; set; }

    public bool Deleted { get; set; }

    public bool Active { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Affiliates")]
    public virtual Address Address { get; set; } = null!;
}
