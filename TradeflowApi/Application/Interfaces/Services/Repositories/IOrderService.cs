using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;
using System.Collections.Generic;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetOrdersAsync(int pageNumber, int pageSize);
    Task<List<OrderDTO>> GetOrderByIdAsync(int id);
    Task<Order> DeleteOrderAsync(int id);
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order);
}