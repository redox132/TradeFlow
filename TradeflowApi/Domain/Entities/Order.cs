using Tradeflow.TradeflowApi.Domain.Enums;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!; // Navigation property
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending; // Enum for order state
    // Total price can be calculated from order items
    public decimal TotalPrice => OrderItems?.Sum(i => i.Quantity * i.Price) ?? 0m;
    // List of products in the order
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
