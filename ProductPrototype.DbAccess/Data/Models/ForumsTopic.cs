using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_Topic")]
[Index("CustomerId", Name = "IX_Forums_Topic_CustomerId")]
[Index("ForumId", Name = "IX_Forums_Topic_ForumId")]
[Index("Subject", Name = "IX_Forums_Topic_Subject")]
public partial class ForumsTopic
{
    [Key]
    public int Id { get; set; }

    public string Subject { get; set; } = null!;

    public int CustomerId { get; set; }

    public int ForumId { get; set; }

    public int TopicTypeId { get; set; }

    public int NumPosts { get; set; }

    public int Views { get; set; }

    public int LastPostId { get; set; }

    public int LastPostCustomerId { get; set; }

    [Precision(6)]
    public DateTime? LastPostTime { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ForumsTopics")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ForumId")]
    [InverseProperty("ForumsTopics")]
    public virtual ForumsForum Forum { get; set; } = null!;

    [InverseProperty("Topic")]
    public virtual ICollection<ForumsPost> ForumsPosts { get; set; } = new List<ForumsPost>();
}
