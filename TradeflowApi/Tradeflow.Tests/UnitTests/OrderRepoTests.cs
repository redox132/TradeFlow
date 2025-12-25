
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Xunit;
using Moq;

namespace Tradeflow.TradeflowApi.Tradeflow.Tests.UnitTests;


public class OrderRepoTests
{
    private readonly IOrderService _orderService;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    public OrderRepoTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderService = new OrderService(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task GetOrdersAsync_shouldReturnOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order { Id = 1, UserId = 42 },
            new Order { Id = 2, UserId = 1337 }
        };

        _orderRepositoryMock
            .Setup(repo => repo.GetOrdersAsync())
            .ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetOrdersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOrderIfExists()
    {
        // Given
        var orderId = 1;
        var order = new Order { Id = orderId, UserId = 42 };
        _orderRepositoryMock
            .Setup(repo => repo.GetOrderByIdAsync(orderId))
            .ReturnsAsync(order);

        // When
        var result = await _orderService.GetOrderByIdAsync(orderId);

        // Then
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }
}