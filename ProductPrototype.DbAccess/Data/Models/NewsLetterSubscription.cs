using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("NewsLetterSubscription")]
[Index("TypeId", Name = "IX_NewsLetterSubscription_TypeId")]
[Index("Email", "StoreId", Name = "IX_NewsletterSubscription_Email_StoreId")]
public partial class NewsLetterSubscription
{
    [Key]
    public int Id { get; set; }

    public int TypeId { get; set; }

    [StringLength(255)]
    public string Email { get; set; } = null!;

    public Guid NewsLetterSubscriptionGuid { get; set; }

    public bool Active { get; set; }

    public int StoreId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    public int LanguageId { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("NewsLetterSubscriptions")]
    public virtual NewsLetterSubscriptionType Type { get; set; } = null!;
}
