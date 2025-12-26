namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Domain.Enums;

public class OrderDTO
{
    public int Id { get; set; }
    public ICollection<OrderItemsDTO> OrderItems { get; set; } = new List<OrderItemsDTO>();
    public CustomerDTO? CustomerDTO { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice =>
        OrderItems?.Sum(i => i.Quantity * i.Price) ?? 0m;
}
