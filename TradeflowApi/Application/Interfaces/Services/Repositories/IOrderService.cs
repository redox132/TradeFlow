using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;
using System.Collections.Generic;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(CreateOrderRequest order);
    Task<Order> DeleteOrderAsync(int id);
}