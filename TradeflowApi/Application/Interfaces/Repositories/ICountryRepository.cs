namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;

public interface ICountryRepository
{
    Task<IEnumerable<CountryDTO>> GetCountriesAsync();
}