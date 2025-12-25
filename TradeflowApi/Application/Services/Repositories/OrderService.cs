namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _orderRepository.GetOrdersAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetOrderByIdAsync(id);
    }
    public async Task<Order> CreateOrderAsync(CreateOrderRequest order)
    {
        return await _orderRepository.CreateOrderAsync(order);
    }

    public async Task<Order> DeleteOrderAsync(int id)
    {
        return await _orderRepository.DeleteOrderAsync(id);
    }
}