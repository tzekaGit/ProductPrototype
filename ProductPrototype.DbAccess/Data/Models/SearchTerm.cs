using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("SearchTerm")]
public partial class SearchTerm
{
    [Key]
    public int Id { get; set; }

    public string? Keyword { get; set; }

    public int StoreId { get; set; }

    public int Count { get; set; }
}
