using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;
using Microsoft.EntityFrameworkCore;

namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CountryDTO>> GetCountriesAsync()
    {
        return await _context.Countries
        .Select(c => new CountryDTO
        {
            Name = c.Name,
            Code = c.Code
        })
        .ToListAsync();
    }
}
