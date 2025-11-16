using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_Subscription")]
[Index("CustomerId", Name = "IX_Forums_Subscription_CustomerId")]
[Index("ForumId", Name = "IX_Forums_Subscription_ForumId")]
[Index("TopicId", Name = "IX_Forums_Subscription_TopicId")]
public partial class ForumsSubscription
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Guid SubscriptionGuid { get; set; }

    public int ForumId { get; set; }

    public int TopicId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ForumsSubscriptions")]
    public virtual Customer Customer { get; set; } = null!;
}
