using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_Post")]
[Index("CustomerId", Name = "IX_Forums_Post_CustomerId")]
[Index("TopicId", Name = "IX_Forums_Post_TopicId")]
public partial class ForumsPost
{
    [Key]
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    [Column("IPAddress")]
    [StringLength(100)]
    public string? Ipaddress { get; set; }

    public int CustomerId { get; set; }

    public int TopicId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    public int VoteCount { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ForumsPosts")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("ForumPost")]
    public virtual ICollection<ForumsPostVote> ForumsPostVotes { get; set; } = new List<ForumsPostVote>();

    [ForeignKey("TopicId")]
    [InverseProperty("ForumsPosts")]
    public virtual ForumsTopic Topic { get; set; } = null!;
}
