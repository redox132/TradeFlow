using Tradeflow.Domain.Entities;

namespace Tradeflow.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int UserId);
    Task<User?> CreateAsync(User user);
}
