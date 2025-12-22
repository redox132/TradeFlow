using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.DTOs.ActionResults;
using Tradeflow.Application.Interfaces.Services;
using Tradeflow.Application.Interfaces.Services.Auth;

namespace Tradeflow.Api.Controllers.Services;

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("products")]
    public IActionResult GetAll()
    {
        return Ok(new {message = "A post to get all products has been invoked."});
    }
}