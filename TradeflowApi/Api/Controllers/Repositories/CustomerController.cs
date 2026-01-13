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
    public async Task<IEnumerable<CustomerDTO>> GetCustomers(int pageNumber = 1, int pageSize = 100)
    {
        var customers = await _customerService.GetCustomers(pageNumber, pageSize);
        return customers;
    }

    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomer(id);
        if (customer == null)
        {
            return Ok( new {message = "No customer found!", status = 404});
        }
        return Ok(customer);
    }

    [HttpPost("customers")]
    public async Task<Customer> AddCustomer([FromBody] CustomerDTO customer)
    {
        var createdCustomer = await _customerService.AddCustomer(customer);
        return createdCustomer;
    }
}