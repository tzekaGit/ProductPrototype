using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("MessageTemplate")]
public partial class MessageTemplate
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string? BccEmailAddresses { get; set; }

    [StringLength(1000)]
    public string? Subject { get; set; }

    public int EmailAccountId { get; set; }

    public string? Body { get; set; }

    public bool IsActive { get; set; }

    public int? DelayBeforeSend { get; set; }

    public int DelayPeriodId { get; set; }

    public int AttachedDownloadId { get; set; }

    public bool AllowDirectReply { get; set; }

    public bool LimitedToStores { get; set; }
}
