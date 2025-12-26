using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Seller?> GetByEmailAsync(string email);
    Task<Seller?> GetByIdAsync(int sellerId);
    Task<Seller?> CreateAsync(Seller seller);
}
