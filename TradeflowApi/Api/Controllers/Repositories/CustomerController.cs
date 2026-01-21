namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers(int pageNumber = 1, int pageSize = 100)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var customers = await _customerService.GetCustomers(pageNumber, pageSize, seller.Id);
        return Ok(customers);
    }

    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var customer = await _customerService.GetCustomer(id, seller.Id);
        if (customer == null)
        {
            return Ok( new {message = "No customer found!", status = 404});
        }
        return Ok(customer);
    }

    [HttpPost("customers")]
    public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customer)
    {
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var createdCustomer = await _customerService.AddCustomer(customer, seller.Id);
        return Ok(createdCustomer);
    }
}