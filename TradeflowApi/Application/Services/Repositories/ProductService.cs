using Tradeflow.TradeflowApi.Application.Interfaces.Services;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Products;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class ProductService : IProductService
{
    public readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public Task<IEnumerable<Product>> GetAllProductsAsync(int sellerId, int pageNumber, int pageSize)
    {
        var products = _productRepository.GetAllAsync(sellerId, pageNumber, pageSize);
        return products;
    }
    // Backwards-compatible overload
    public Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize)
    {
        return _productRepository.GetAllAsync(pageNumber, pageSize);
    }
    public Task<Product?> GetProductByIdAsync(int sellerId, int id)
    {
        var product = _productRepository.GetByIdAsync(sellerId, id);
        return product;
    }
    // Backwards-compatible overload
    public Task<Product?> GetProductByIdAsync(int id)
    {
        return _productRepository.GetByIdAsync(id);
    }
    public Task CreateProductAsync(CreateProductRequest product)
    {
        var createdProduct = _productRepository.CreateAsync(product);
        return createdProduct;
        
    }
    public Task UpdateProductAsync(int id, CreateProductRequest product)
    {
        var updatedProduct = _productRepository.UpdateAsync(id, product);
        return updatedProduct;
    }
    public Task DeleteProductAsync(int id)
    {
        var updatedProduct = _productRepository.DeleteByIdAsync(id);
        return updatedProduct;
    }
}