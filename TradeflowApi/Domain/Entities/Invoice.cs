namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Invoice
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
    public string Number { get; set; } = null!;

    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
