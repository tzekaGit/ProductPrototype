using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("VendorNote")]
[Index("VendorId", Name = "IX_VendorNote_VendorId")]
public partial class VendorNote
{
    [Key]
    public int Id { get; set; }

    public string Note { get; set; } = null!;

    public int VendorId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("VendorId")]
    [InverseProperty("VendorNotes")]
    public virtual Vendor Vendor { get; set; } = null!;
}
