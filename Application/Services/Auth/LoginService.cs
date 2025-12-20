using Tradeflow.Application.Interfaces.Auth;

namespace Tradeflow.Application.Services.Auth;

public class LoginService : ILoginService
{
    private readonly ITokenService _tokenService;

    public LoginService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public string Login(string email, string password)
    {
        return _tokenService.GenerateToken(1, email);
    }
}
