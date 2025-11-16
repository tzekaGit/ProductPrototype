using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("CrossSellProduct")]
public partial class CrossSellProduct
{
    [Key]
    public int Id { get; set; }

    public int ProductId1 { get; set; }

    public int ProductId2 { get; set; }
}
