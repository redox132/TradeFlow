using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderShipments;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class OrderShipmentService : IOrderShipmentService
{
    private readonly IOrderShipmentRepository _repo;

    public OrderShipmentService(IOrderShipmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<OrderShipmentDTO>> GetOrderShipmentsAsync(int sellerId, int pageNumber, int pageSize)
    {
        return await _repo.GetOrderShipmentsAsync(sellerId, pageNumber, pageSize);
    }

    public async Task<List<OrderShipmentDTO>> GetOrderShipmentsAsync(int pageNumber, int pageSize)
    {
        return await _repo.GetOrderShipmentsAsync(pageNumber, pageSize);
    }

    public async Task<OrderShipmentDTO?> GetOrderShipmentByIdAsync(int sellerId, int id)
    {
        return await _repo.GetOrderShipmentByIdAsync(sellerId, id);
    }

    public async Task<OrderShipmentDTO?> GetOrderShipmentByIdAsync(int id)
    {
        return await _repo.GetOrderShipmentByIdAsync(id);
    }

    public async Task<OrderShipment> CreateOrderShipmentAsync(CreateOrderShipmentRequest request, int sellerId)
    {
        return await _repo.CreateOrderShipmentAsync(request, sellerId);
    }

    public async Task<OrderShipment> CreateOrderShipmentAsync(CreateOrderShipmentRequest request)
    {
        return await _repo.CreateOrderShipmentAsync(request);
    }

    public async Task<OrderShipment?> UpdateOrderShipmentAsync(int sellerId, int id, CreateOrderShipmentRequest request)
    {
        return await _repo.UpdateOrderShipmentAsync(sellerId, id, request);
    }

    public async Task<OrderShipment?> UpdateOrderShipmentAsync(int id, CreateOrderShipmentRequest request)
    {
        return await _repo.UpdateOrderShipmentAsync(id, request);
    }

    public async Task<OrderShipment?> DeleteOrderShipmentAsync(int sellerId, int id)
    {
        return await _repo.DeleteOrderShipmentAsync(sellerId, id);
    }

    public async Task<OrderShipment?> DeleteOrderShipmentAsync(int id)
    {
        return await _repo.DeleteOrderShipmentAsync(id);
    }
}
