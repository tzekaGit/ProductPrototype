using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Poll")]
[Index("LanguageId", Name = "IX_Poll_LanguageId")]
public partial class Poll
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LanguageId { get; set; }

    public string? SystemKeyword { get; set; }

    public bool Published { get; set; }

    public bool ShowOnHomepage { get; set; }

    public bool AllowGuestsToVote { get; set; }

    public int DisplayOrder { get; set; }

    public bool LimitedToStores { get; set; }

    [Precision(6)]
    public DateTime? StartDateUtc { get; set; }

    [Precision(6)]
    public DateTime? EndDateUtc { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("Polls")]
    public virtual Language Language { get; set; } = null!;

    [InverseProperty("Poll")]
    public virtual ICollection<PollAnswer> PollAnswers { get; set; } = new List<PollAnswer>();
}
