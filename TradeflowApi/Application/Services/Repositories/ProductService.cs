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