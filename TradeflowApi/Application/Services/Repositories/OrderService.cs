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

    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(int sellerId, int pageNumber, int pageSize)
    {
        return await _orderRepository.GetOrdersAsync(sellerId, pageNumber, pageSize);
    }
    // Backwards-compatible overload
    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(int pageNumber, int pageSize)
    {
        return await _orderRepository.GetOrdersAsync(0, pageNumber, pageSize);
    }

    public async Task<List<OrderDTO>> GetOrderByIdAsync(int sellerId, int id)
    {
        return await _orderRepository.GetOrderByIdAsync(sellerId, id);
    }
    // Backwards-compatible overload
    public async Task<List<OrderDTO>> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetOrderByIdAsync(0, id);
    }

    public async Task<Order> DeleteOrderAsync(int sellerId, int id)
    {
        return await _orderRepository.DeleteOrderAsync(sellerId, id);
    }
    // Backwards-compatible overload
    public async Task<Order> DeleteOrderAsync(int id)
    {
        return await _orderRepository.DeleteOrderAsync(0, id);
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order, int sellerId)
    {
        return await _orderRepository.CreateOrderAsync(order, sellerId);
    }
    // Backwards-compatible overload
    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order)
    {
        return await _orderRepository.CreateOrderAsync(order, 0);
    }
}