namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.ShipmentMethods;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ShipmentMethodRepository : IShipmentMethodRepository
{
    private readonly AppDbContext _context;

    public ShipmentMethodRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ShipmentMethodDTO>> GetShipmentMethodsAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _context.ShipmentMethods
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new ShipmentMethodDTO
            {
                Id = s.Id,
                Title = s.Title,
                Integration = s.Integration,
                Price = s.Price,
                FreeFrom = s.FreeFrom,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<ShipmentMethodDTO?> GetShipmentMethodByIdAsync(int id)
    {
        var s = await _context.ShipmentMethods.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (s == null) return null;
        return new ShipmentMethodDTO
        {
            Id = s.Id,
            Title = s.Title,
            Integration = s.Integration,
            Price = s.Price,
            FreeFrom = s.FreeFrom,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }

    public async Task<ShipmentMethod> CreateShipmentMethodAsync(CreateShipmentMethodRequest request)
    {
        var entity = new ShipmentMethod
        {
            Title = request.Title,
            Integration = request.Integration,
            Price = request.Price,
            FreeFrom = request.FreeFrom,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        _context.ShipmentMethods.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<ShipmentMethod?> UpdateShipmentMethodAsync(int id, CreateShipmentMethodRequest request)
    {
        var entity = await _context.ShipmentMethods.FindAsync(id);
        if (entity == null) return null;

        entity.Title = request.Title;
        entity.Integration = request.Integration;
        entity.Price = request.Price;
        entity.FreeFrom = request.FreeFrom;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ShipmentMethod?> DeleteShipmentMethodAsync(int id)
    {
        var entity = await _context.ShipmentMethods.FindAsync(id);
        if (entity == null) return null;
        _context.ShipmentMethods.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
