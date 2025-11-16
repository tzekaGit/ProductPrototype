using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Keyless]
[Table("MigrationVersionInfo")]
public partial class MigrationVersionInfo
{
    public long Version { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppliedOn { get; set; }

    [StringLength(1024)]
    public string? Description { get; set; }
}
