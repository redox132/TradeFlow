namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;

public interface ILoginService
{
    Task<string> LoginAsync(string email, string password);
}
