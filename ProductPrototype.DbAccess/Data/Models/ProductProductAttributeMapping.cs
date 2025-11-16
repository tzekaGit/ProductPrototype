using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Product_ProductAttribute_Mapping")]
[Index("ProductAttributeId", Name = "IX_Product_ProductAttribute_Mapping_ProductAttributeId")]
[Index("ProductId", Name = "IX_Product_ProductAttribute_Mapping_ProductId")]
[Index("ProductId", "DisplayOrder", Name = "IX_Product_ProductAttribute_Mapping_ProductId_DisplayOrder")]
public partial class ProductProductAttributeMapping
{
    [Key]
    public int Id { get; set; }

    public int ProductAttributeId { get; set; }

    public int ProductId { get; set; }

    public string? TextPrompt { get; set; }

    public bool IsRequired { get; set; }

    public int AttributeControlTypeId { get; set; }

    public int DisplayOrder { get; set; }

    public int? ValidationMinLength { get; set; }

    public int? ValidationMaxLength { get; set; }

    public string? ValidationFileAllowedExtensions { get; set; }

    public int? ValidationFileMaximumSize { get; set; }

    public string? DefaultValue { get; set; }

    public string? ConditionAttributeXml { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductProductAttributeMappings")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ProductAttributeId")]
    [InverseProperty("ProductProductAttributeMappings")]
    public virtual ProductAttribute ProductAttribute { get; set; } = null!;

    [InverseProperty("ProductAttributeMapping")]
    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
}
