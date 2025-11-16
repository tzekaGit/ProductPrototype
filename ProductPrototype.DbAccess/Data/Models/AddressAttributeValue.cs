using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("AddressAttributeValue")]
[Index("AddressAttributeId", Name = "IX_AddressAttributeValue_AddressAttributeId")]
public partial class AddressAttributeValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public int AddressAttributeId { get; set; }

    public bool IsPreSelected { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("AddressAttributeId")]
    [InverseProperty("AddressAttributeValues")]
    public virtual AddressAttribute AddressAttribute { get; set; } = null!;
}
