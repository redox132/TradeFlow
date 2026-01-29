using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Suppliers;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _repo;

    public SupplierService(ISupplierRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SupplierDTO>> GetSuppliersAsync(int sellerId)
    {
        return await _repo.GetSuppliersAsync(sellerId);
    }
}
