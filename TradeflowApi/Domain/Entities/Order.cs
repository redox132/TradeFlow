using Tradeflow.TradeflowApi.Domain.Enums;
namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending; // Enum for order state
    public decimal TotalPrice => OrderItems?.Sum(i => i.Quantity * i.Price) ?? 0m; // Total price can be calculated from order items on run time
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
