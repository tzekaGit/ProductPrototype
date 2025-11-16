using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("GdprConsent")]
public partial class GdprConsent
{
    [Key]
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    public bool IsRequired { get; set; }

    public string? RequiredMessage { get; set; }

    public bool DisplayDuringRegistration { get; set; }

    public bool DisplayOnCustomerInfoPage { get; set; }

    public int DisplayOrder { get; set; }
}
