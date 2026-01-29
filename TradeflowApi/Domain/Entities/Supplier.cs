namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? Phone { get; set; }
    public int SellerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
