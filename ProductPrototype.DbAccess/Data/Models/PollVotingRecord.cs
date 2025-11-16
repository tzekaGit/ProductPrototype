using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("PollVotingRecord")]
[Index("CustomerId", Name = "IX_PollVotingRecord_CustomerId")]
[Index("PollAnswerId", Name = "IX_PollVotingRecord_PollAnswerId")]
public partial class PollVotingRecord
{
    [Key]
    public int Id { get; set; }

    public int PollAnswerId { get; set; }

    public int CustomerId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("PollVotingRecords")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("PollAnswerId")]
    [InverseProperty("PollVotingRecords")]
    public virtual PollAnswer PollAnswer { get; set; } = null!;
}
