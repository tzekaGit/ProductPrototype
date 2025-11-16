using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("GoogleAuthenticatorRecord")]
public partial class GoogleAuthenticatorRecord
{
    [Key]
    public int Id { get; set; }

    public string? Customer { get; set; }

    public string? SecretKey { get; set; }
}
