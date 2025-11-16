using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("StateProvince")]
[Index("CountryId", Name = "IX_StateProvince_CountryId")]
public partial class StateProvince
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Abbreviation { get; set; }

    public int CountryId { get; set; }

    public bool Published { get; set; }

    public int DisplayOrder { get; set; }

    [InverseProperty("StateProvince")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("CountryId")]
    [InverseProperty("StateProvinces")]
    public virtual Country Country { get; set; } = null!;
}
