using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("GenericAttribute")]
[Index("EntityId", "KeyGroup", Name = "IX_GenericAttribute_EntityId_and_KeyGroup")]
public partial class GenericAttribute
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string KeyGroup { get; set; } = null!;

    [StringLength(400)]
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int EntityId { get; set; }

    public int StoreId { get; set; }

    [Column("CreatedOrUpdatedDateUTC")]
    [Precision(6)]
    public DateTime? CreatedOrUpdatedDateUtc { get; set; }
}
