using Tradeflow.Domain.Entities;

namespace Tradeflow.Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize);
    Task<Product?> GetProductByIdAsync(int id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}