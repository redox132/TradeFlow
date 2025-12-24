using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> DeleteByIdAsync(int productId);
    Task<Product?> UpdateAsync(Product product);
    Task<Product?> CreateAsync(Product product);
}
