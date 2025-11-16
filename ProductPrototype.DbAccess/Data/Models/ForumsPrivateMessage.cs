using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_PrivateMessage")]
[Index("FromCustomerId", Name = "IX_Forums_PrivateMessage_FromCustomerId")]
[Index("ToCustomerId", Name = "IX_Forums_PrivateMessage_ToCustomerId")]
public partial class ForumsPrivateMessage
{
    [Key]
    public int Id { get; set; }

    [StringLength(450)]
    public string Subject { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int FromCustomerId { get; set; }

    public int ToCustomerId { get; set; }

    public int StoreId { get; set; }

    public bool IsRead { get; set; }

    public bool IsDeletedByAuthor { get; set; }

    public bool IsDeletedByRecipient { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("FromCustomerId")]
    [InverseProperty("ForumsPrivateMessageFromCustomers")]
    public virtual Customer FromCustomer { get; set; } = null!;

    [ForeignKey("ToCustomerId")]
    [InverseProperty("ForumsPrivateMessageToCustomers")]
    public virtual Customer ToCustomer { get; set; } = null!;
}
