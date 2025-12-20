namespace Tradeflow.Application.Interfaces.Auth;

public interface ITokenService
{
    string GenerateToken(int userId, string email);
}
