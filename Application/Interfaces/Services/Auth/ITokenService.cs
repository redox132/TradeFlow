namespace Tradeflow.Application.Interfaces.Services.Auth;

public interface ITokenService
{
    string GenerateToken(int userId, string email);
}
