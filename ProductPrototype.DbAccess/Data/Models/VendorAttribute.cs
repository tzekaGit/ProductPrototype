using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("VendorAttribute")]
public partial class VendorAttribute
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public bool IsRequired { get; set; }

    public int AttributeControlTypeId { get; set; }

    public int DisplayOrder { get; set; }

    [InverseProperty("VendorAttribute")]
    public virtual ICollection<VendorAttributeValue> VendorAttributeValues { get; set; } = new List<VendorAttributeValue>();
}
