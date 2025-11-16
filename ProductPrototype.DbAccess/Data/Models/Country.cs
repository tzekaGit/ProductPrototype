using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Country")]
[Index("DisplayOrder", Name = "IX_Country_DisplayOrder")]
public partial class Country
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(2)]
    public string? TwoLetterIsoCode { get; set; }

    [StringLength(3)]
    public string? ThreeLetterIsoCode { get; set; }

    public bool AllowsBilling { get; set; }

    public bool AllowsShipping { get; set; }

    public int NumericIsoCode { get; set; }

    public bool SubjectToVat { get; set; }

    public bool Published { get; set; }

    public int DisplayOrder { get; set; }

    public bool LimitedToStores { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("Country")]
    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();

    [ForeignKey("CountryId")]
    [InverseProperty("Countries")]
    public virtual ICollection<ShippingMethod> ShippingMethods { get; set; } = new List<ShippingMethod>();
}
