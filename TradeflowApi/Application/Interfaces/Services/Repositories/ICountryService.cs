using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

public interface ICountryService
{
    Task<IEnumerable<CountryDTO>> GetCountriesAsync();
}
