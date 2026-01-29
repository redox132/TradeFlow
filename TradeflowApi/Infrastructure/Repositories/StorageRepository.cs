namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Storages;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class StorageRepository : IStorageRepository
{
    private readonly AppDbContext _context;

    public StorageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StorageDTO>> GetStoragesAsync(int sellerId, int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _context.Set<Storage>()
            .Where(s => s.SellerId == sellerId)
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StorageDTO
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Capacity = s.Capacity,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<StorageDTO?> GetStorageByIdAsync(int sellerId, int id)
    {
        var s = await _context.Set<Storage>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (s == null) return null;
        return new StorageDTO
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
            Capacity = s.Capacity,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }

    public async Task<Storage> CreateStorageAsync(CreateStorageRequest request, int sellerId)
    {
        var entity = new Storage
        {
            Name = request.Name,
            Address = request.Address,
            Capacity = request.Capacity,
            SellerId = sellerId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Set<Storage>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Storage?> UpdateStorageAsync(int sellerId, int id, CreateStorageRequest request)
    {
        var entity = await _context.Set<Storage>().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (entity == null) return null;

        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.Capacity = request.Capacity;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Storage?> DeleteStorageAsync(int sellerId, int id)
    {
        var entity = await _context.Set<Storage>().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (entity == null) return null;
        _context.Set<Storage>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
