using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetCountriesAsync();
}
