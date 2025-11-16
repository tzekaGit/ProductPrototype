using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("PermissionRecord")]
public partial class PermissionRecord
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string SystemName { get; set; } = null!;

    [StringLength(255)]
    public string Category { get; set; } = null!;

    [ForeignKey("PermissionRecordId")]
    [InverseProperty("PermissionRecords")]
    public virtual ICollection<CustomerRole> CustomerRoles { get; set; } = new List<CustomerRole>();
}
