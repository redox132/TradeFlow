namespace Tradeflow.DTOs.ProductVariant;

public class VariantUpdateDto
{
    public int Id { get; set; }
    public string? CatalogNumber { get; set; }
    public string? EAN { get; set; }
    public string? Symbol { get; set; }
}