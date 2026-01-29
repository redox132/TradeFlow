using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Suppliers;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface ISupplierRepository
{
    Task<List<SupplierDTO>> GetSuppliersAsync(int sellerId);
}
