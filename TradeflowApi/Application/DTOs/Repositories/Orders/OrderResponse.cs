namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Domain.Enums;

public class OrderResponse
{
    public ICollection<OrderItemsDTO> OrderItems { get; set; } = new List<OrderItemsDTO>();
    public CustomerDTO CustomerDTO { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
