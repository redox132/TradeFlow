using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.ActionResults;
using Tradeflow.TradeflowApi.Application.Interfaces.Services;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Products;

namespace Tradeflow.TradeflowApi.Api.Controllers.Services;

[ApiController]
[Route("api")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync(1, 100);
        OK ok = new OK
        {
            Status = 200,
            Message = "No products found",
        };

        if (products.Any())
            return Ok(products);
        return Ok(ok);
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost("products")]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest product)
    {
        await _productService.CreateProductAsync(product);
        return Ok(product);
    }

    [HttpPut("products/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProductRequest product)
    {
        await _productService.UpdateProductAsync(id, product);
        return NoContent();
    }

    [HttpDelete("products/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}