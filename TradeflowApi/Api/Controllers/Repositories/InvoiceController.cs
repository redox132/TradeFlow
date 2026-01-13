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

    [HttpDelete("invoice/{invoiceId}")]
    public async Task<IActionResult> DeleteInvoice(int invoiceId)
    {
        var isDeleted = await _invoiceService.DeleteInvoice(invoiceId);
        if (isDeleted)
        {
            return Ok(new { message = "Invoice has been deleted!", status = 200 });
        }
        return NotFound(new { message = "Invoice has not been found!", status = 404 });
    }
}