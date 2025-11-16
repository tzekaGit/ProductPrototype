using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("QueuedEmail")]
[Index("CreatedOnUtc", Name = "IX_QueuedEmail_CreatedOnUtc", AllDescending = true)]
[Index("EmailAccountId", Name = "IX_QueuedEmail_EmailAccountId")]
[Index("SentOnUtc", "DontSendBeforeDateUtc", Name = "IX_QueuedEmail_SentOnUtc_DontSendBeforeDateUtc_Extended")]
public partial class QueuedEmail
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    public string From { get; set; } = null!;

    [StringLength(500)]
    public string? FromName { get; set; }

    [StringLength(500)]
    public string To { get; set; } = null!;

    [StringLength(500)]
    public string? ToName { get; set; }

    [StringLength(500)]
    public string? ReplyTo { get; set; }

    [StringLength(500)]
    public string? ReplyToName { get; set; }

    [Column("CC")]
    [StringLength(500)]
    public string? Cc { get; set; }

    [StringLength(500)]
    public string? Bcc { get; set; }

    [StringLength(1000)]
    public string? Subject { get; set; }

    public int EmailAccountId { get; set; }

    public int PriorityId { get; set; }

    public string? Body { get; set; }

    public string? AttachmentFilePath { get; set; }

    public string? AttachmentFileName { get; set; }

    public int AttachedDownloadId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime? DontSendBeforeDateUtc { get; set; }

    public int SentTries { get; set; }

    [Precision(6)]
    public DateTime? SentOnUtc { get; set; }

    [ForeignKey("EmailAccountId")]
    [InverseProperty("QueuedEmails")]
    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
