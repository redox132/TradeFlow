namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Returns;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

[ApiController]
[Route("api/returns")]
public class OrderReturnController : ControllerBase
{
    private readonly IOrderReturnService _service;

    public OrderReturnController(IOrderReturnService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetReturns(int pageSize = 1, int pageNumber = 100)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var items = await _service.GetOrderReturnsAsync(seller.Id, pageSize, pageNumber);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReturn(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var item = await _service.GetOrderReturnByIdAsync(seller.Id, id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReturn([FromBody] CreateOrderReturnRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var created = await _service.CreateOrderReturnAsync(request, seller.Id);
        return Ok(new { id = created.Id, createdAt = created.CreatedAt });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReturn(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var deleted = await _service.DeleteOrderReturnAsync(seller.Id, id);
        if (deleted == null) return NotFound();
        return Ok(new { message = "Return deleted" });
    }
}
