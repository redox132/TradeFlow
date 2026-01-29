using Tradeflow.TradeflowApi.Application.DTOs.Repositories.ShipmentMethods;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

public interface IShipmentMethodService
{
    Task<List<ShipmentMethodDTO>> GetShipmentMethodsAsync(int pageNumber, int pageSize);
    Task<ShipmentMethodDTO?> GetShipmentMethodByIdAsync(int id);
    Task<ShipmentMethod> CreateShipmentMethodAsync(CreateShipmentMethodRequest request);
    Task<ShipmentMethod?> UpdateShipmentMethodAsync(int id, CreateShipmentMethodRequest request);
    Task<ShipmentMethod?> DeleteShipmentMethodAsync(int id);
}
