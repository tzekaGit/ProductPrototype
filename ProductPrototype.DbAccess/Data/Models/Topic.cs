using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Topic")]
[Index("AvailableEndDateTimeUtc", "AvailableStartDateTimeUtc", Name = "IX_Topic_Availability", AllDescending = true)]
[Index("SystemName", Name = "IX_Topic_SystemName")]
public partial class Topic
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string? SystemName { get; set; }

    public bool IncludeInSitemap { get; set; }

    public int DisplayOrder { get; set; }

    public bool AccessibleWhenStoreClosed { get; set; }

    public bool IsPasswordProtected { get; set; }

    public string? Password { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public bool Published { get; set; }

    public int TopicTemplateId { get; set; }

    public string? MetaKeywords { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaTitle { get; set; }

    public bool SubjectToAcl { get; set; }

    public bool LimitedToStores { get; set; }

    [Precision(6)]
    public DateTime? AvailableStartDateTimeUtc { get; set; }

    [Precision(6)]
    public DateTime? AvailableEndDateTimeUtc { get; set; }
}
