using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("CustomWishlist")]
[Index("CustomerId", Name = "IX_CustomWishlist_CustomerId")]
public partial class CustomWishlist
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Name { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomWishlists")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("CustomWishlist")]
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
}
