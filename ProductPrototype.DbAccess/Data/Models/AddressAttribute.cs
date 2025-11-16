using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("AddressAttribute")]
public partial class AddressAttribute
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public bool IsRequired { get; set; }

    public int AttributeControlTypeId { get; set; }

    public int DisplayOrder { get; set; }

    [InverseProperty("AddressAttribute")]
    public virtual ICollection<AddressAttributeValue> AddressAttributeValues { get; set; } = new List<AddressAttributeValue>();
}
