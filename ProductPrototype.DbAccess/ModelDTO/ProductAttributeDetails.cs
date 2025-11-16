using System;
using System.Collections.Generic;
using System.Text;

namespace ProductPrototype.DbAccess.ModelDTO;

public class ProductDetailDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string? Sku { get; set; }
    public string? Length { get; set; }
    public decimal  Price { get; set; }
    public decimal Weight { get; set; }
    public int GroupBySpecificationId { get; set; }
    public int SpecificationOptionId { get; set; }
    public string SpecificationOptionName { get; set; }
    public bool IsRequired { get; set; }
}
