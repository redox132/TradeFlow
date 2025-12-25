using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
        return await _context.Countries
            .AsNoTracking()
            .ToListAsync();
    }
}
  