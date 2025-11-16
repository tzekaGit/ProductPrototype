using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ExternalAuthenticationRecord")]
[Index("CustomerId", Name = "IX_ExternalAuthenticationRecord_CustomerId")]
public partial class ExternalAuthenticationRecord
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Email { get; set; }

    public string? ExternalIdentifier { get; set; }

    public string? ExternalDisplayIdentifier { get; set; }

    [Column("OAuthToken")]
    public string? OauthToken { get; set; }

    [Column("OAuthAccessToken")]
    public string? OauthAccessToken { get; set; }

    public string? ProviderSystemName { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ExternalAuthenticationRecords")]
    public virtual Customer Customer { get; set; } = null!;
}
