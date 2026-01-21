namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;

public interface ICountryRepository
{
    Task<IEnumerable<CountryDTO>> GetCountriesAsync(int sellerUserId);
    // Backwards-compatible overload (no sellerId) for tests/older callers
    Task<IEnumerable<CountryDTO>> GetCountriesAsync();
}
