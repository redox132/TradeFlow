using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Returns;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class OrderReturnService : IOrderReturnService
{
    private readonly IOrderReturnRepository _repo;

    public OrderReturnService(IOrderReturnRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<OrderReturnDTO>> GetOrderReturnsAsync(int sellerId, int pageNumber, int pageSize)
    {
        return await _repo.GetOrderReturnsAsync(sellerId, pageNumber, pageSize);
    }

    public async Task<OrderReturnDTO?> GetOrderReturnByIdAsync(int sellerId, int id)
    {
        return await _repo.GetOrderReturnByIdAsync(sellerId, id);
    }

    public async Task<OrderReturn> CreateOrderReturnAsync(CreateOrderReturnRequest request, int sellerId)
    {
        return await _repo.CreateOrderReturnAsync(request, sellerId);
    }

    public async Task<OrderReturn?> DeleteOrderReturnAsync(int sellerId, int id)
    {
        return await _repo.DeleteOrderReturnAsync(sellerId, id);
    }
}
