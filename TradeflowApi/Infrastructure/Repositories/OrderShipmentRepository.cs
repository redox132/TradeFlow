namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderShipments;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderShipmentRepository : IOrderShipmentRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderShipmentRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrderShipmentDTO>> GetOrderShipmentsAsync(int sellerId, int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _appDbContext.Set<OrderShipment>()
            .Where(s => s.SellerId == sellerId)
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new OrderShipmentDTO
            {
                Id = s.Id,
                OrderId = s.OrderId,
                TrackingNumber = s.TrackingNumber,
                Carrier = s.Carrier,
                ShippedAt = s.ShippedAt,
                DeliveredAt = s.DeliveredAt,
                Status = s.Status,
                SellerId = s.SellerId,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<List<OrderShipmentDTO>> GetOrderShipmentsAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _appDbContext.Set<OrderShipment>()
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new OrderShipmentDTO
            {
                Id = s.Id,
                OrderId = s.OrderId,
                TrackingNumber = s.TrackingNumber,
                Carrier = s.Carrier,
                ShippedAt = s.ShippedAt,
                DeliveredAt = s.DeliveredAt,
                Status = s.Status,
                SellerId = s.SellerId,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<OrderShipmentDTO?> GetOrderShipmentByIdAsync(int sellerId, int id)
    {
        var s = await _appDbContext.Set<OrderShipment>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (s == null) return null;
        return new OrderShipmentDTO
        {
            Id = s.Id,
            OrderId = s.OrderId,
            TrackingNumber = s.TrackingNumber,
            Carrier = s.Carrier,
            ShippedAt = s.ShippedAt,
            DeliveredAt = s.DeliveredAt,
            Status = s.Status,
            SellerId = s.SellerId,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }

    public async Task<OrderShipmentDTO?> GetOrderShipmentByIdAsync(int id)
    {
        var s = await _appDbContext.Set<OrderShipment>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (s == null) return null;
        return new OrderShipmentDTO
        {
            Id = s.Id,
            OrderId = s.OrderId,
            TrackingNumber = s.TrackingNumber,
            Carrier = s.Carrier,
            ShippedAt = s.ShippedAt,
            DeliveredAt = s.DeliveredAt,
            Status = s.Status,
            SellerId = s.SellerId,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }

    public async Task<OrderShipment> CreateOrderShipmentAsync(CreateOrderShipmentRequest request, int sellerId)
    {
        var entity = new OrderShipment
        {
            OrderId = request.OrderId,
            TrackingNumber = request.TrackingNumber,
            Carrier = request.Carrier,
            ShippedAt = request.ShippedAt,
            DeliveredAt = request.DeliveredAt,
            Status = request.Status,
            SellerId = sellerId,
            CreatedAt = DateTime.UtcNow
        };

        _appDbContext.Set<OrderShipment>().Add(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<OrderShipment> CreateOrderShipmentAsync(CreateOrderShipmentRequest request)
    {
        var entity = new OrderShipment
        {
            OrderId = request.OrderId,
            TrackingNumber = request.TrackingNumber,
            Carrier = request.Carrier,
            ShippedAt = request.ShippedAt,
            DeliveredAt = request.DeliveredAt,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        _appDbContext.Set<OrderShipment>().Add(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<OrderShipment?> UpdateOrderShipmentAsync(int sellerId, int id, CreateOrderShipmentRequest request)
    {
        var entity = await _appDbContext.Set<OrderShipment>().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (entity == null) return null;

        entity.OrderId = request.OrderId;
        entity.TrackingNumber = request.TrackingNumber;
        entity.Carrier = request.Carrier;
        entity.ShippedAt = request.ShippedAt;
        entity.DeliveredAt = request.DeliveredAt;
        entity.Status = request.Status;
        entity.UpdatedAt = DateTime.UtcNow;

        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderShipment?> UpdateOrderShipmentAsync(int id, CreateOrderShipmentRequest request)
    {
        var entity = await _appDbContext.Set<OrderShipment>().FindAsync(id);
        if (entity == null) return null;

        entity.OrderId = request.OrderId;
        entity.TrackingNumber = request.TrackingNumber;
        entity.Carrier = request.Carrier;
        entity.ShippedAt = request.ShippedAt;
        entity.DeliveredAt = request.DeliveredAt;
        entity.Status = request.Status;
        entity.UpdatedAt = DateTime.UtcNow;

        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderShipment?> DeleteOrderShipmentAsync(int sellerId, int id)
    {
        var entity = await _appDbContext.Set<OrderShipment>().FirstOrDefaultAsync(x => x.Id == id && x.SellerId == sellerId);
        if (entity == null) return null;
        _appDbContext.Set<OrderShipment>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderShipment?> DeleteOrderShipmentAsync(int id)
    {
        var entity = await _appDbContext.Set<OrderShipment>().FindAsync(id);
        if (entity == null) return null;
        _appDbContext.Set<OrderShipment>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }
}
