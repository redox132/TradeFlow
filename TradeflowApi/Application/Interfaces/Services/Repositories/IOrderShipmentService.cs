using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderShipments;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

public interface IOrderShipmentService
{
    Task<List<OrderShipmentDTO>> GetOrderShipmentsAsync(int sellerId, int pageNumber, int pageSize);
    Task<List<OrderShipmentDTO>> GetOrderShipmentsAsync(int pageNumber, int pageSize);

    Task<OrderShipmentDTO?> GetOrderShipmentByIdAsync(int sellerId, int id);
    Task<OrderShipmentDTO?> GetOrderShipmentByIdAsync(int id);

    Task<OrderShipment> CreateOrderShipmentAsync(CreateOrderShipmentRequest request, int sellerId);
    Task<OrderShipment> CreateOrderShipmentAsync(CreateOrderShipmentRequest request);

    Task<OrderShipment?> UpdateOrderShipmentAsync(int sellerId, int id, CreateOrderShipmentRequest request);
    Task<OrderShipment?> UpdateOrderShipmentAsync(int id, CreateOrderShipmentRequest request);

    Task<OrderShipment?> DeleteOrderShipmentAsync(int sellerId, int id);
    Task<OrderShipment?> DeleteOrderShipmentAsync(int id);
}
