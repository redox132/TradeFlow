using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface ICountryRepository
{
    Task<IEnumerable<CountryDTO>> GetCountriesAsync();
}