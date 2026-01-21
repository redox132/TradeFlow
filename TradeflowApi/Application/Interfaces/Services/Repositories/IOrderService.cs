using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;
using System.Collections.Generic;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetOrdersAsync(int sellerId, int pageNumber, int pageSize);
    // Backwards-compatible overload
    Task<IEnumerable<OrderDTO>> GetOrdersAsync(int pageNumber, int pageSize);

    Task<List<OrderDTO>> GetOrderByIdAsync(int sellerId, int id);
    // Backwards-compatible overload
    Task<List<OrderDTO>> GetOrderByIdAsync(int id);

    Task<Order> DeleteOrderAsync(int sellerId, int id);
    // Backwards-compatible overload
    Task<Order> DeleteOrderAsync(int id);

    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order, int sellerId);
    // Backwards-compatible overload (no sellerId -> create without setting SellerId)
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order);
}
