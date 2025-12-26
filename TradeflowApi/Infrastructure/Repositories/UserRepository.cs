using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Infrastructure.Data;

namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Seller?> GetByEmailAsync(string email)
    {
        return await _context.Sellers.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Seller?> GetByIdAsync(int id)
    {
        return await _context.Sellers.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Seller?> CreateAsync(Seller seller)
    {
        await _context.Sellers.AddAsync(seller);
        await _context.SaveChangesAsync();
        return seller;
    }
}
