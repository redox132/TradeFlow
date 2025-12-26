using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

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

    public async Task<Seller?> RegisterAsync(string name, string email, string password)
    {
        var sellerExists = await _userRepository.GetByEmailAsync(email);
        if (sellerExists != null)
        {
            return null;
        }

        var seller = new Seller
        {
            Name = name,
            Email = email,
            Password = _passwordService.HashPassword(password)
        };

        return await _userRepository.CreateAsync(seller);
    }
}
