namespace Tradeflow.TradeflowApi.Infrastructure.Repositories.Auth;

using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApiKeyRepository : IApiKeyRepository
{
    private readonly AppDbContext _context;

    public ApiKeyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiKey?> GetApiKey(string apiKey)
    {
        return await _context.ApiKeys
            .Include(k => k.Seller)
            .AsNoTracking()
            .FirstOrDefaultAsync(k => k.Key == apiKey && !k.IsRevoked);
    }

}
