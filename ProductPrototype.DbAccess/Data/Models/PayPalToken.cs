using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("PayPalToken")]
[Index("CustomerId", Name = "IX_PayPalToken_CustomerId")]
public partial class PayPalToken
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [StringLength(100)]
    public string? VaultId { get; set; }

    [StringLength(100)]
    public string? VaultCustomerId { get; set; }

    [StringLength(100)]
    public string? TransactionId { get; set; }

    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? Expiration { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(200)]
    public string ClientId { get; set; } = null!;

    public bool IsPrimaryMethod { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("PayPalTokens")]
    public virtual Customer Customer { get; set; } = null!;
}
