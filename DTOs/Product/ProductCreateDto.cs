using System.ComponentModel.DataAnnotations;
using Tradeflow.DTOs.ProductVariant;
public class ProductCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }
    public string? CatalogNumber { get; set; }
    public string? EAN { get; set; }
    public string? Symbol { get; set; }
    public string? Location { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public List<ProductVariantDto>? Variants { get; set; }
}