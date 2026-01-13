namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;

public interface IInvoiceService
{
    Task<List<GetInvoiceRequest>> GetInvoices(int pageNUmber, int pageSize);
    Task<List<InvoiceDTO>> GetInvoice(int id);
    Task<InvoiceDTO> CreateInvoice(CreateInvoiceRequest request);
    Task<bool> DeleteInvoice(int invoiceId);
}