using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("UrlRecord")]
[Index("EntityId", "EntityName", "LanguageId", "IsActive", Name = "IX_UrlRecord_Custom_1")]
[Index("Slug", Name = "IX_UrlRecord_Slug")]
public partial class UrlRecord
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string EntityName { get; set; } = null!;

    [StringLength(400)]
    public string Slug { get; set; } = null!;

    public int EntityId { get; set; }

    public bool IsActive { get; set; }

    public int LanguageId { get; set; }
}
