namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<GetInvoiceRequest>> GetInvoices(int pageNumber, int pageSize)
    {
        return await _invoiceRepository.GetInvoices(pageNumber, pageSize);
    }

    public async Task<List<InvoiceDTO>> GetInvoice(int id)
    {
        return await _invoiceRepository.GetInvoice(id);
    }

    public async Task<InvoiceDTO> CreateInvoice(CreateInvoiceRequest request)
    {
        return await _invoiceRepository.CreateInvoice(request);
    }

    public async Task<bool> DeleteInvoice(int invoiceId)
    {
        return await _invoiceRepository.DeleteInvoice(invoiceId);
    }
}