namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;
using Microsoft.EntityFrameworkCore;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CountryDTO>> GetCountriesAsync(int SellerId)
    {
        return await _context.Countries
        .Where(c => c.SellerId == SellerId)
        .Select(c => new CountryDTO
        {
            Name = c.Name,
            Code = c.Code
        })
        .ToListAsync() ?? new List<CountryDTO>(); 

    }

    // Backwards-compatible overload: return unscoped set (sellerId = 0 => no match) or all if you prefer.
    public async Task<IEnumerable<CountryDTO>> GetCountriesAsync()
    {
        // For compatibility with existing tests which call GetCountriesAsync() without sellerId,
        // return all countries (no seller filter) — this preserves previous behavior.
        return await _context.Countries
            .Select(c => new CountryDTO
            {
                Name = c.Name,
                Code = c.Code
            })
            .ToListAsync() ?? new List<CountryDTO>();
    }
}
