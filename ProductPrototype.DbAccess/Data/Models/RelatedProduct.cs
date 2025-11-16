using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("RelatedProduct")]
[Index("ProductId1", Name = "IX_RelatedProduct_ProductId1")]
public partial class RelatedProduct
{
    [Key]
    public int Id { get; set; }

    public int ProductId1 { get; set; }

    public int ProductId2 { get; set; }

    public int DisplayOrder { get; set; }
}
