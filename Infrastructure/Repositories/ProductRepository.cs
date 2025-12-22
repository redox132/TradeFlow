using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tradeflow.Application.Interfaces.Repositories;
using Tradeflow.Domain.Entities;
using Tradeflow.Infrastructure.Data;
using Tradeflow.Application.DTOs.ActionResults;

namespace Tradeflow.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize < 1 ? 10 : pageSize;

        return await _context.Products
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Product?> DeleteByIdAsync(int id)
    {
        // 1. Find the product
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return null;

        // 2. Remove the entity
        _context.Products.Remove(product);

        // 3. Save changes
        await _context.SaveChangesAsync();

        // Return the deleted product
        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product?> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
