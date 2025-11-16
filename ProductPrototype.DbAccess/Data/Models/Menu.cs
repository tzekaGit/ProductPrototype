using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Menu")]
public partial class Menu
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    [StringLength(400)]
    public string? CssClass { get; set; }

    public int MenuTypeId { get; set; }

    public int DisplayOrder { get; set; }

    public bool Published { get; set; }

    public bool SubjectToAcl { get; set; }

    public bool LimitedToStores { get; set; }

    public bool Deleted { get; set; }

    public bool DisplayAllCategories { get; set; }

    [InverseProperty("Menu")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
