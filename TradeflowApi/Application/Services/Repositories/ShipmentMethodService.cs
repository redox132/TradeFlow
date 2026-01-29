using Tradeflow.TradeflowApi.Application.DTOs.Repositories.ShipmentMethods;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class ShipmentMethodService : IShipmentMethodService
{
    private readonly IShipmentMethodRepository _repo;

    public ShipmentMethodService(IShipmentMethodRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ShipmentMethodDTO>> GetShipmentMethodsAsync(int pageNumber, int pageSize)
    {
        return await _repo.GetShipmentMethodsAsync(pageNumber, pageSize);
    }

    public async Task<ShipmentMethodDTO?> GetShipmentMethodByIdAsync(int id)
    {
        return await _repo.GetShipmentMethodByIdAsync(id);
    }

    public async Task<ShipmentMethod> CreateShipmentMethodAsync(CreateShipmentMethodRequest request)
    {
        return await _repo.CreateShipmentMethodAsync(request);
    }

    public async Task<ShipmentMethod?> UpdateShipmentMethodAsync(int id, CreateShipmentMethodRequest request)
    {
        return await _repo.UpdateShipmentMethodAsync(id, request);
    }

    public async Task<ShipmentMethod?> DeleteShipmentMethodAsync(int id)
    {
        return await _repo.DeleteShipmentMethodAsync(id);
    }
}
