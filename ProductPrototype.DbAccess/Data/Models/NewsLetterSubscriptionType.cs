using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("NewsLetterSubscriptionType")]
public partial class NewsLetterSubscriptionType
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public bool TickedByDefault { get; set; }

    public int DisplayOrder { get; set; }

    public bool LimitedToStores { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<NewsLetterSubscription> NewsLetterSubscriptions { get; set; } = new List<NewsLetterSubscription>();
}
