using Tradeflow.Application.Interfaces.Auth;
using Tradeflow.Application.Interfaces.Repositories;

namespace Tradeflow.Application.Services.Auth;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;

    public LoginService(IUserRepository userRepository, ITokenService tokenService, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return "Invalid Creds";

        if (!_passwordService.VerifyPassword(user.Password, password))
        {
            return "Invalid Creds";
        }

        return _tokenService.GenerateToken(user.Id, user.Email);
    }
}
