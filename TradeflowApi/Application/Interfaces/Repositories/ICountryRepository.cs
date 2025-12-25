using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetCountriesAsync();
}