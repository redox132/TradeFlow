using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Suppliers;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

public interface ISupplierService
{
    Task<List<SupplierDTO>> GetSuppliersAsync(int sellerId);
}
