using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<List<OrderDTO>> GetOrdersAsync(int pageNumber, int pageSize);
    Task<List<OrderDTO>> GetOrderByIdAsync(int id);
    Task<Order> DeleteOrderAsync(int id);
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order);
}