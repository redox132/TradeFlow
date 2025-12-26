namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;

public interface IInvoiceRepository
{
    Task<List<GetInvoiceRequest>> GetInvoices(int pageNUmber, int pageSize);
    Task<List<InvoiceDTO>> GetInvoice(int id);
    Task<InvoiceDTO> CreateInvoice(CreateInvoiceRequest request);
}
