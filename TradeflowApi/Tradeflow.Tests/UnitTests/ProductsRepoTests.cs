using Moq;
using System.Net;
using System.Net.Http;
using Tradeflow.TradeflowApi.Application.Services.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Tests.UnitTests;

public class ProductsRepoTests
{
    private readonly ProductService _productService;
    private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

    public ProductsRepoTests()
    {
        _productService = new ProductService(_productRepositoryMock.Object);
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_should_return_all_products()
    {

        var mock = _productRepositoryMock.Setup(repo => repo.GetAllAsync(1, 100))
            .ReturnsAsync(new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            });

        var products = await _productService.GetAllProductsAsync(1, 100);

        Assert.Equal(2, products.Count());
        Assert.Equal("Product 1", products.First().Name);
    }

    [Fact]
    public async Task GetByIdAsync_shouldReturnNull_whenProductDoesNotExist()
    {
        _productRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product?)null);

        var product = await _productService.GetProductByIdAsync(99);

        Assert.Null(product);
    }
    
    
}
