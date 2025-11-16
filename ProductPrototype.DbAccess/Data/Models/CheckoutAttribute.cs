using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("CheckoutAttribute")]
public partial class CheckoutAttribute
{
    [Key]
    public int Id { get; set; }

    [StringLength(400)]
    public string Name { get; set; } = null!;

    public string? TextPrompt { get; set; }

    public bool ShippableProductRequired { get; set; }

    public bool IsTaxExempt { get; set; }

    public int TaxCategoryId { get; set; }

    public bool LimitedToStores { get; set; }

    public int? ValidationMinLength { get; set; }

    public int? ValidationMaxLength { get; set; }

    public string? ValidationFileAllowedExtensions { get; set; }

    public int? ValidationFileMaximumSize { get; set; }

    public string? DefaultValue { get; set; }

    public string? ConditionAttributeXml { get; set; }

    public bool IsRequired { get; set; }

    public int AttributeControlTypeId { get; set; }

    public int DisplayOrder { get; set; }

    [InverseProperty("CheckoutAttribute")]
    public virtual ICollection<CheckoutAttributeValue> CheckoutAttributeValues { get; set; } = new List<CheckoutAttributeValue>();
}
