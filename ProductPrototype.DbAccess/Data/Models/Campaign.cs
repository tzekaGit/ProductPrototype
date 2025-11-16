using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Campaign")]
public partial class Campaign
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int StoreId { get; set; }

    public int CustomerRoleId { get; set; }

    public int NewsLetterSubscriptionTypeId { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime? DontSendBeforeDateUtc { get; set; }
}
