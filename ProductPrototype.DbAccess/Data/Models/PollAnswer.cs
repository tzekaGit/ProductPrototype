using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("PollAnswer")]
[Index("PollId", Name = "IX_PollAnswer_PollId")]
public partial class PollAnswer
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PollId { get; set; }

    public int NumberOfVotes { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("PollId")]
    [InverseProperty("PollAnswers")]
    public virtual Poll Poll { get; set; } = null!;

    [InverseProperty("PollAnswer")]
    public virtual ICollection<PollVotingRecord> PollVotingRecords { get; set; } = new List<PollVotingRecord>();
}
