namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderShipments;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

[ApiController]
[Route("api")]
public class OrderShipmentController : ControllerBase
{
    private readonly IOrderShipmentService _service;

    public OrderShipmentController(IOrderShipmentService service)
    {
        _service = service;
    }

    [HttpGet("ordersshipments")]
    public async Task<IActionResult> GetOrderShipments(int pageSize = 1, int pageNumber = 100)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var list = await _service.GetOrderShipmentsAsync(seller.Id, pageSize, pageNumber);
        return Ok(list);
    }

    [HttpGet("ordersshipments/{id}")]
    public async Task<IActionResult> GetOrderShipment(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var item = await _service.GetOrderShipmentByIdAsync(seller.Id, id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost("ordersshipments")]
    public async Task<IActionResult> CreateOrderShipment([FromBody] CreateOrderShipmentRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var created = await _service.CreateOrderShipmentAsync(request, seller.Id);
        var response = new OrderShipmentResponse
        {
            Id = created.Id,
            OrderId = created.OrderId,
            TrackingNumber = created.TrackingNumber,
            Carrier = created.Carrier,
            ShippedAt = created.ShippedAt,
            DeliveredAt = created.DeliveredAt,
            Status = created.Status,
            CreatedAt = created.CreatedAt
        };

        return Ok(response);
    }

    [HttpPut("ordersshipments/{id}")]
    public async Task<IActionResult> UpdateOrderShipment(int id, [FromBody] CreateOrderShipmentRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var updated = await _service.UpdateOrderShipmentAsync(seller.Id, id, request);
        if (updated == null) return NotFound();

        var response = new OrderShipmentResponse
        {
            Id = updated.Id,
            OrderId = updated.OrderId,
            TrackingNumber = updated.TrackingNumber,
            Carrier = updated.Carrier,
            ShippedAt = updated.ShippedAt,
            DeliveredAt = updated.DeliveredAt,
            Status = updated.Status,
            CreatedAt = updated.CreatedAt
        };

        return Ok(response);
    }

    [HttpDelete("ordersshipments/{id}")]
    public async Task<IActionResult> DeleteOrderShipment(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var deleted = await _service.DeleteOrderShipmentAsync(seller.Id, id);
        if (deleted == null) return NotFound();

        return Ok(new { message = "Order shipment deleted successfully" });
    }
}
