using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("StoreMapping")]
[Index("EntityId", "EntityName", Name = "IX_StoreMapping_EntityId_EntityName")]
[Index("StoreId", Name = "IX_StoreMapping_StoreId")]
public partial class StoreMapping
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string EntityName { get; set; } = null!;

    public int StoreId { get; set; }

    public int EntityId { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("StoreMappings")]
    public virtual Store Store { get; set; } = null!;
}
