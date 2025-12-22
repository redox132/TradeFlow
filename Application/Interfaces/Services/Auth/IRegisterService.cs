using Tradeflow.Domain.Entities;

namespace Tradeflow.Application.Interfaces.Services.Auth;

public interface IRegisterService
{
    Task<User?> RegisterAsync(string name, string email, string password);
}
