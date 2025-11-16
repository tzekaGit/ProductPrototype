using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("Product_SpecificationAttribute_Mapping")]
[Index("AllowFiltering", Name = "IX_PSAM_AllowFiltering")]
[Index("SpecificationAttributeOptionId", "AllowFiltering", Name = "IX_PSAM_SpecificationAttributeOptionId_AllowFiltering")]
[Index("ProductId", Name = "IX_Product_SpecificationAttribute_Mapping_ProductId")]
[Index("SpecificationAttributeOptionId", Name = "IX_Product_SpecificationAttribute_Mapping_SpecificationAttributeOptionId")]
public partial class ProductSpecificationAttributeMapping
{
    [Key]
    public int Id { get; set; }

    [StringLength(4000)]
    public string? CustomValue { get; set; }

    public int ProductId { get; set; }

    public int SpecificationAttributeOptionId { get; set; }

    public int AttributeTypeId { get; set; }

    public bool AllowFiltering { get; set; }

    public bool ShowOnProductPage { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductSpecificationAttributeMappings")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("SpecificationAttributeOptionId")]
    [InverseProperty("ProductSpecificationAttributeMappings")]
    public virtual SpecificationAttributeOption SpecificationAttributeOption { get; set; } = null!;
}
