using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_Group")]
[Index("DisplayOrder", Name = "IX_Forums_Group_DisplayOrder")]
public partial class ForumsGroup
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    public int DisplayOrder { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    [InverseProperty("ForumGroup")]
    public virtual ICollection<ForumsForum> ForumsForums { get; set; } = new List<ForumsForum>();
}
