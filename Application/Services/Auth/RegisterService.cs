using Tradeflow.Application.Interfaces.Auth;
using Tradeflow.Application.Interfaces.Repositories;
using Tradeflow.Domain.Entities;

namespace Tradeflow.Application.Services.Auth;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public RegisterService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<User?> RegisterAsync(string name, string email, string password)
    {
        var userExists = await _userRepository.GetByEmailAsync(email);
        if (userExists != null)
        {
            return null;
        }

        var user = new User
        {
            Name = name,
            Email = email,
            Password = _passwordService.HashPassword(password)
        };

        return await _userRepository.CreateAsync(user);
    }
}
