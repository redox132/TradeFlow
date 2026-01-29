namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Suppliers;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SupplierDTO>> GetSuppliersAsync(int sellerId)
    {
        return await _context.Set<Supplier>()
            .Where(s => s.SellerId == sellerId)
            .AsNoTracking()
            .Select(s => new SupplierDTO
            {
                Id = s.Id,
                Name = s.Name,
                ContactEmail = s.ContactEmail,
                Phone = s.Phone
            })
            .ToListAsync();
    }
}
