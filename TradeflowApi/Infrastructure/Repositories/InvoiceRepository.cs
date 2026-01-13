namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Users;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _appDbContext;

    public InvoiceRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<GetInvoiceRequest>> GetInvoices(int pageNUmber, int pageSize)
    {
        return await _appDbContext.Invoices
        .AsNoTracking()
        .Skip((pageNUmber - 1) * pageSize)
        .Take(pageSize)
        .Select(i => new GetInvoiceRequest
        {
            Id = i.Id,
            Type = i.Type,
            Number = i.Number
        }).ToListAsync();
    }

    public async Task<List<InvoiceDTO>> GetInvoice(int id)
    {
        return await _appDbContext.Invoices
        .Where(i => i.Id == id)
        .Select(i => new InvoiceDTO
        {
            Type = i.Type,
            Number = i.Number
        }).ToListAsync();
    }

    public async Task<InvoiceDTO> CreateInvoice(CreateInvoiceRequest request)
    {
        var invoiceEntity = new Invoice
        {
            Type = request.Invoice.Type,
            Number = request.Invoice.Number
        };

        _appDbContext.Invoices.Add(invoiceEntity);
        await _appDbContext.SaveChangesAsync();

        return new InvoiceDTO
        {
            Type = invoiceEntity.Type,
            Number = invoiceEntity.Number
        };
    }

    public async Task<bool> DeleteInvoice(int invoiceId)
    {
        var invoice = await _appDbContext.Invoices.FindAsync(invoiceId);
        if (invoice != null)
        {
            _appDbContext.Invoices.Remove(invoice);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}