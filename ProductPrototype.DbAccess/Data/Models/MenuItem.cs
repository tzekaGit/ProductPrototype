using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("MenuItem")]
[Index("MenuId", Name = "IX_MenuItem_MenuId")]
public partial class MenuItem
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string? Title { get; set; }

    [StringLength(400)]
    public string? CssClass { get; set; }

    [StringLength(400)]
    public string? RouteName { get; set; }

    public int MenuId { get; set; }

    public string? Url { get; set; }

    public int MenuItemTypeId { get; set; }

    public int? EntityId { get; set; }

    public int? NumberOfSubItemsPerGridElement { get; set; }

    public int? NumberOfItemsPerGridRow { get; set; }

    public int? MaximumNumberEntities { get; set; }

    public int TemplateId { get; set; }

    public int DisplayOrder { get; set; }

    public bool SubjectToAcl { get; set; }

    public bool LimitedToStores { get; set; }

    public bool Published { get; set; }

    public int ParentId { get; set; }

    [ForeignKey("MenuId")]
    [InverseProperty("MenuItems")]
    public virtual Menu Menu { get; set; } = null!;
}
