using Tradeflow.TradeflowApi.Domain.Enums;
namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public int SellerId { get; set; }
    // Backwards-compatibility: some tests and older code expect UserId
    public int UserId { get => SellerId; set => SellerId = value; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public OrderStatus Status { get; set; }
}
