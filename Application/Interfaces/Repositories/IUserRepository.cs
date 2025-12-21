using Tradeflow.Domain.Entities;

namespace Tradeflow.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}
