using Microsoft.EntityFrameworkCore;
using Tradeflow.Application.Interfaces.Repositories;
using Tradeflow.Domain.Entities;
using Tradeflow.Infrastructure.Data;

namespace Tradeflow.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
