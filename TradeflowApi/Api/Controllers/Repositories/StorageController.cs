namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Storages;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

[ApiController]
[Route("api/storages")]
public class StorageController : ControllerBase
{
    private readonly IStorageService _service;

    public StorageController(IStorageService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetStorages(int pageSize = 1, int pageNumber = 100)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var items = await _service.GetStoragesAsync(seller.Id, pageSize, pageNumber);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStorage(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var item = await _service.GetStorageByIdAsync(seller.Id, id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStorage([FromBody] CreateStorageRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var created = await _service.CreateStorageAsync(request, seller.Id);
        return Ok(new { id = created.Id, createdAt = created.CreatedAt });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStorage(int id, [FromBody] CreateStorageRequest request)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var updated = await _service.UpdateStorageAsync(seller.Id, id, request);
        if (updated == null) return NotFound();
        return Ok(new { id = updated.Id, updatedAt = updated.UpdatedAt });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStorage(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var deleted = await _service.DeleteStorageAsync(seller.Id, id);
        if (deleted == null) return NotFound();
        return Ok(new { message = "Storage deleted" });
    }
}
