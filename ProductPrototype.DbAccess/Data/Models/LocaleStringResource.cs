using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("LocaleStringResource")]
[Index("ResourceName", "LanguageId", Name = "IX_LocaleStringResource")]
[Index("LanguageId", Name = "IX_LocaleStringResource_LanguageId")]
public partial class LocaleStringResource
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string ResourceName { get; set; } = null!;

    public string ResourceValue { get; set; } = null!;

    public int LanguageId { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("LocaleStringResources")]
    public virtual Language Language { get; set; } = null!;
}
