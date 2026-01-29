namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Returns;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderReturnRepository : IOrderReturnRepository
{
    private readonly AppDbContext _context;

    public OrderReturnRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderReturnDTO>> GetOrderReturnsAsync(int sellerId, int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _context.Set<OrderReturn>()
            .Where(r => r.SellerId == sellerId)
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new OrderReturnDTO
            {
                Id = r.Id,
                OrderId = r.OrderId,
                ProductId = r.ProductId,
                Quantity = r.Quantity,
                Reason = r.Reason,
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<OrderReturnDTO?> GetOrderReturnByIdAsync(int sellerId, int id)
    {
        var r = await _context.Set<OrderReturn>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (r == null) return null;
        return new OrderReturnDTO
        {
            Id = r.Id,
            OrderId = r.OrderId,
            ProductId = r.ProductId,
            Quantity = r.Quantity,
            Reason = r.Reason,
            Status = r.Status,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        };
    }

    public async Task<OrderReturn> CreateOrderReturnAsync(CreateOrderReturnRequest request, int sellerId)
    {
        var entity = new OrderReturn
        {
            OrderId = request.OrderId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Reason = request.Reason,
            SellerId = sellerId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Set<OrderReturn>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderReturn?> DeleteOrderReturnAsync(int sellerId, int id)
    {
        var entity = await _context.Set<OrderReturn>().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (entity == null) return null;
        _context.Set<OrderReturn>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
