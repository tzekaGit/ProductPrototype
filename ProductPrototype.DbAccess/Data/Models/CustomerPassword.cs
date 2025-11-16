using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("CustomerPassword")]
[Index("CustomerId", Name = "IX_CustomerPassword_CustomerId")]
public partial class CustomerPassword
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Password { get; set; }

    public int PasswordFormatId { get; set; }

    public string? PasswordSalt { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerPasswords")]
    public virtual Customer Customer { get; set; } = null!;
}
