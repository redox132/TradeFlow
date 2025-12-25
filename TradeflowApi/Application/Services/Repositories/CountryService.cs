using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
        return await _countryRepository.GetCountriesAsync();
    }
}
