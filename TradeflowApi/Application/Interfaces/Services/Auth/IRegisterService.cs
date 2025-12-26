using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;

public interface IRegisterService
{
    Task<Seller?> RegisterAsync(string name, string email, string password);
}
