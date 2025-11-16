using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Forums_PostVote")]
[Index("ForumPostId", Name = "IX_Forums_PostVote_ForumPostId")]
public partial class ForumsPostVote
{
    [Key]
    public int Id { get; set; }

    public int ForumPostId { get; set; }

    public int CustomerId { get; set; }

    public bool IsUp { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("ForumPostId")]
    [InverseProperty("ForumsPostVotes")]
    public virtual ForumsPost ForumPost { get; set; } = null!;
}
