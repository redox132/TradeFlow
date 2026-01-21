using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;
public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<CountryDTO>> GetCountriesAsync(int sellerUserId)
    {
        return await _countryRepository.GetCountriesAsync(sellerUserId);
    }

    // Backwards-compatible overload
    public async Task<IEnumerable<CountryDTO>> GetCountriesAsync()
    {
        return await _countryRepository.GetCountriesAsync();
    }
}
