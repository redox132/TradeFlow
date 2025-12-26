namespace TradeflowApi.TradeflowApi.Domain.Entities;

public class ApiKey
{
    public int Id { get; set; }
    public required string KeyHash { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRevoked { get; set; }
}
