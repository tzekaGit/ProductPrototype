using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Address")]
[Index("CountryId", Name = "IX_Address_CountryId")]
[Index("StateProvinceId", Name = "IX_Address_StateProvinceId")]
public partial class Address
{
    [Key]
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public int? StateProvinceId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Company { get; set; }

    public string? County { get; set; }

    public string? City { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? ZipPostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FaxNumber { get; set; }

    public string? CustomAttributes { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Affiliate> Affiliates { get; set; } = new List<Affiliate>();

    [ForeignKey("CountryId")]
    [InverseProperty("Addresses")]
    public virtual Country? Country { get; set; }

    [InverseProperty("BillingAddress")]
    public virtual ICollection<Customer> CustomerBillingAddresses { get; set; } = new List<Customer>();

    [InverseProperty("ShippingAddress")]
    public virtual ICollection<Customer> CustomerShippingAddresses { get; set; } = new List<Customer>();

    [InverseProperty("BillingAddress")]
    public virtual ICollection<Order> OrderBillingAddresses { get; set; } = new List<Order>();

    [InverseProperty("PickupAddress")]
    public virtual ICollection<Order> OrderPickupAddresses { get; set; } = new List<Order>();

    [InverseProperty("ShippingAddress")]
    public virtual ICollection<Order> OrderShippingAddresses { get; set; } = new List<Order>();

    [ForeignKey("StateProvinceId")]
    [InverseProperty("Addresses")]
    public virtual StateProvince? StateProvince { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Addresses")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
