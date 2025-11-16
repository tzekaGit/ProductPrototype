using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("AclRecord")]
[Index("CustomerRoleId", Name = "IX_AclRecord_CustomerRoleId")]
[Index("EntityId", "EntityName", Name = "IX_AclRecord_EntityId_EntityName")]
public partial class AclRecord
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string EntityName { get; set; } = null!;

    public int CustomerRoleId { get; set; }

    public int EntityId { get; set; }

    [ForeignKey("CustomerRoleId")]
    [InverseProperty("AclRecords")]
    public virtual CustomerRole CustomerRole { get; set; } = null!;
}
