namespace Tradeflow.TradeflowApi.Application.Services.Auth;

using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

public class ApiKeyService : IApiKeyService
{
    private readonly IApiKeyRepository _apiKeyRepository;

    public ApiKeyService(IApiKeyRepository apiKeyRepository)
    {
        _apiKeyRepository = apiKeyRepository;
    }

    public async Task<ApiKey?> GetApiKey(string apiKey)
    {
        return await _apiKeyRepository.GetApiKey(apiKey);
    }
}
