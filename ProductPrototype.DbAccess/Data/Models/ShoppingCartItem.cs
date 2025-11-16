using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductPrototype.DbAccess.Data.Models;

[Table("ShoppingCartItem")]
[Index("CustomWishlistId", Name = "IX_ShoppingCartItem_CustomWishlistId")]
[Index("CustomerId", Name = "IX_ShoppingCartItem_CustomerId")]
[Index("ProductId", Name = "IX_ShoppingCartItem_ProductId")]
[Index("ShoppingCartTypeId", "CustomerId", Name = "IX_ShoppingCartItem_ShoppingCartTypeId_CustomerId")]
public partial class ShoppingCartItem
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int? CustomWishlistId { get; set; }

    public int StoreId { get; set; }

    public int ShoppingCartTypeId { get; set; }

    public string? AttributesXml { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal CustomerEnteredPrice { get; set; }

    public int Quantity { get; set; }

    [Precision(6)]
    public DateTime? RentalStartDateUtc { get; set; }

    [Precision(6)]
    public DateTime? RentalEndDateUtc { get; set; }

    [Precision(6)]
    public DateTime CreatedOnUtc { get; set; }

    [Precision(6)]
    public DateTime UpdatedOnUtc { get; set; }

    [ForeignKey("CustomWishlistId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual CustomWishlist? CustomWishlist { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual Product Product { get; set; } = null!;
}
