using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Returns;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IOrderReturnRepository
{
    Task<List<OrderReturnDTO>> GetOrderReturnsAsync(int sellerId, int pageNumber, int pageSize);
    Task<OrderReturnDTO?> GetOrderReturnByIdAsync(int sellerId, int id);
    Task<OrderReturn> CreateOrderReturnAsync(CreateOrderReturnRequest request, int sellerId);
    Task<OrderReturn?> DeleteOrderReturnAsync(int sellerId, int id);
}
