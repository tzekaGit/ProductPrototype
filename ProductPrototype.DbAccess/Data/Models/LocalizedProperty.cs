using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("LocalizedProperty")]
[Index("LanguageId", Name = "IX_LocalizedProperty_LanguageId")]
public partial class LocalizedProperty
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string LocaleKeyGroup { get; set; } = null!;

    [StringLength(400)]
    public string LocaleKey { get; set; } = null!;

    public string LocaleValue { get; set; } = null!;

    public int LanguageId { get; set; }

    public int EntityId { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("LocalizedProperties")]
    public virtual Language Language { get; set; } = null!;
}
