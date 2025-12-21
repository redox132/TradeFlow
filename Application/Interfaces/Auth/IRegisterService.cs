using Tradeflow.Domain.Entities;

namespace Tradeflow.Application.Interfaces.Auth;

public interface IRegisterService
{
    Task<User?> RegisterAsync(string name, string email, string password);
}
