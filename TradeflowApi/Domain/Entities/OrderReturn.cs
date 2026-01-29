namespace Tradeflow.TradeflowApi.Domain.Entities;

public class OrderReturn
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = null!;
    public string Status { get; set; } = "pending";
    public int SellerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public Order? Order { get; set; }
}
