using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("EmailAccount")]
public partial class EmailAccount
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? DisplayName { get; set; }

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Host { get; set; } = null!;

    [StringLength(255)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public int Port { get; set; }

    public bool EnableSsl { get; set; }

    public int MaxNumberOfEmails { get; set; }

    public int EmailAuthenticationMethodId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? TenantId { get; set; }

    [InverseProperty("EmailAccount")]
    public virtual ICollection<QueuedEmail> QueuedEmails { get; set; } = new List<QueuedEmail>();
}
