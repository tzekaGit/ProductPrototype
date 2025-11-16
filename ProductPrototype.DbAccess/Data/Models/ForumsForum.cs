using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_Forum")]
[Index("DisplayOrder", Name = "IX_Forums_Forum_DisplayOrder")]
[Index("ForumGroupId", Name = "IX_Forums_Forum_ForumGroupId")]
public partial class ForumsForum
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    public int ForumGroupId { get; set; }

    public string? Description { get; set; }

    public int NumTopics { get; set; }

    public int NumPosts { get; set; }

    public int LastTopicId { get; set; }

    public int LastPostId { get; set; }

    public int LastPostCustomerId { get; set; }

    [Precision(6)]
    public DateTime? LastPostTime { get; set; }

    public int DisplayOrder { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    [ForeignKey("ForumGroupId")]
    [InverseProperty("ForumsForums")]
    public virtual ForumsGroup ForumGroup { get; set; } = null!;

    [InverseProperty("Forum")]
    public virtual ICollection<ForumsTopic> ForumsTopics { get; set; } = new List<ForumsTopic>();
}
