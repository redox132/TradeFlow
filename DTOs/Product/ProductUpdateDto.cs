namespace Tradeflow.DTOs.Product;

using Tradeflow.DTOs.ProductVariant;

public class ProductUpdateDto
{
    public string? Name { get; set; }
    public string? CatalogNumber { get; set; }
    public string? EAN { get; set; }
    public string? Symbol { get; set; }
    public string? Location { get; set; }
    public int? Stock { get; set; }
    public decimal? Price { get; set; }
    public List<VariantUpdateDto>? Variants { get; set; }
}