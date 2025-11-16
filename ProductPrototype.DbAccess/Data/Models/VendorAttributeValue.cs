using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("VendorAttributeValue")]
[Index("VendorAttributeId", Name = "IX_VendorAttributeValue_VendorAttributeId")]
public partial class VendorAttributeValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public int VendorAttributeId { get; set; }

    public bool IsPreSelected { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("VendorAttributeId")]
    [InverseProperty("VendorAttributeValues")]
    public virtual VendorAttribute VendorAttribute { get; set; } = null!;
}
