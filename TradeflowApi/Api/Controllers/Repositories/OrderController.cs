namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Domain.Entities;

[ApiController]
[Route("api")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders(int pageSize = 1, int pageNumber = 100)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var orders = await _orderService.GetOrdersAsync(seller.Id, pageSize, pageNumber);
        return Ok(orders);
    }

    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

    var order = await _orderService.GetOrderByIdAsync(seller.Id, id);
        return Ok(order);
    }


    [HttpDelete("order/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

    var order = await _orderService.GetOrderByIdAsync(seller.Id, id);
        if (order == null || !order.Any())
        {
            return NotFound();
        }

        await _orderService.DeleteOrderAsync(seller.Id, id);

        return Ok(new { message = "Order deleted successfully" });
    }

    [HttpPost("order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest order)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var createdOrder = await _orderService.CreateOrderAsync(order, seller.Id);
        return Ok(createdOrder);
    }
}