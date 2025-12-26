namespace Tradeflow.TradeflowApi.Api.Controllers;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class InvoiceController : ControllerBase
{

    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("invoices")]
    public async Task<IActionResult> GetInvoices(int pageNumber = 1, int pageSize = 100)
    {
        var invoices = await _invoiceService.GetInvoices(pageNumber, pageSize);
        return Ok(invoices);
    }

    [HttpGet("invoice/{id}")]
    public async Task<IActionResult> GetInvoices(int id)
    {
        var invoice = await _invoiceService.GetInvoice(id);
        return Ok(invoice);
    }

    [HttpPost("invoices")]
    public async Task<IActionResult> CreateInvoice(CreateInvoiceRequest request)
    {
        var invoice = await _invoiceService.CreateInvoice(request);
        return Ok(invoice);
    }
}