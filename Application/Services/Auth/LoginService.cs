using Tradeflow.Application.Interfaces.Auth;
using Tradeflow.Application.Interfaces.Repositories;

namespace Tradeflow.Application.Services.Auth;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return "Invalid Creds";

        // TODO: verify password hash
        // if (!VerifyPassword(password, user.PasswordHash)) ...

        return _tokenService.GenerateToken(user.Id, user.Email);
    }
}
