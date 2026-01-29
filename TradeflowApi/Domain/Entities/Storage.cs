namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Storage
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int Capacity { get; set; }
    public int SellerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
