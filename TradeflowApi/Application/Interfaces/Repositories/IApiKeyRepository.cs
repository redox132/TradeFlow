namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Domain.Entities;

public interface IApiKeyRepository
{
    Task<ApiKey?> GetApiKey(string apiKey);
}