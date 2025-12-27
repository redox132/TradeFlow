namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;

using Tradeflow.TradeflowApi.Domain.Entities;

public interface IApiKeyService
{
    Task<ApiKey?> GetApiKey(string apiKey);
}