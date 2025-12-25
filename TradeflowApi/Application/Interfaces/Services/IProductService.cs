using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Products;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize);
    Task<Product?> GetProductByIdAsync(int id);
    Task CreateProductAsync(CreateProductRequest product);
    Task UpdateProductAsync(int id, CreateProductRequest product);
    Task DeleteProductAsync(int id);
}