using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Products;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);
    Task<Product?> GetByIdAsync(int id);
    Task<CreateProductRequest?> CreateAsync(CreateProductRequest product);
    Task<CreateProductRequest?> UpdateAsync(int id, CreateProductRequest product);
    Task<Product?> DeleteByIdAsync(int productId);
}
