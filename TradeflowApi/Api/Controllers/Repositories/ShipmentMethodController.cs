namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.ShipmentMethods;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

[ApiController]
[Route("api/shipments")]
public class ShipmentMethodController : ControllerBase
{
    private readonly IShipmentMethodService _service;

    public ShipmentMethodController(IShipmentMethodService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetShipmentMethods(int pageSize = 1, int pageNumber = 100)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller)
            return Unauthorized();

        var list = await _service.GetShipmentMethodsAsync(pageSize, pageNumber);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShipmentMethod(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller)
            return Unauthorized();

        var item = await _service.GetShipmentMethodByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShipmentMethod([FromBody] CreateShipmentMethodRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller)
            return Unauthorized();

        var created = await _service.CreateShipmentMethodAsync(request);
        var response = new ShipmentMethodDTO
        {
            Id = created.Id,
            Title = created.Title,
            Integration = created.Integration,
            Price = created.Price,
            FreeFrom = created.FreeFrom,
            IsActive = created.IsActive,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShipmentMethod(int id, [FromBody] CreateShipmentMethodRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller)
            return Unauthorized();

        var updated = await _service.UpdateShipmentMethodAsync(id, request);
        if (updated == null) return NotFound();

        var response = new ShipmentMethodDTO
        {
            Id = updated.Id,
            Title = updated.Title,
            Integration = updated.Integration,
            Price = updated.Price,
            FreeFrom = updated.FreeFrom,
            IsActive = updated.IsActive,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipmentMethod(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller)
            return Unauthorized();

        var deleted = await _service.DeleteShipmentMethodAsync(id);
        if (deleted == null) return NotFound();

        return Ok(new { message = "Shipment method deleted successfully" });
    }
}
