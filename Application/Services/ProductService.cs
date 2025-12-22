using Tradeflow.Application.Interfaces.Services;
using Tradeflow.Domain.Entities;
using Tradeflow.Application.Interfaces.Repositories;

namespace Tradeflow.Application.Services;

public class ProductService : IProductService
{
    public readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize)
    {
        var products = _productRepository.GetAllAsync(pageNumber, pageSize);
        return products;
    }
    public Task<Product?> GetProductByIdAsync(int id)
    {
        var product = _productRepository.GetByIdAsync(id);
        return product;
    }
    public Task CreateProductAsync(Product product)
    {
        var createdProduct = _productRepository.CreateAsync(product);
        return createdProduct;
        
    }
    public Task UpdateProductAsync(Product product)
    {
        var updatedProduct = _productRepository.UpdateAsync(product);
        return updatedProduct;
    }
    public Task DeleteProductAsync(int id)
    {
        var updatedProduct = _productRepository.DeleteByIdAsync(id);
        return updatedProduct;
    }
}