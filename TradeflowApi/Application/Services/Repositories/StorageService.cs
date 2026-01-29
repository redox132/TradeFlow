using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Storages;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Services.Repositories;

public class StorageService : IStorageService
{
    private readonly IStorageRepository _repo;

    public StorageService(IStorageRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<StorageDTO>> GetStoragesAsync(int sellerId, int pageNumber, int pageSize)
    {
        return await _repo.GetStoragesAsync(sellerId, pageNumber, pageSize);
    }

    public async Task<StorageDTO?> GetStorageByIdAsync(int sellerId, int id)
    {
        return await _repo.GetStorageByIdAsync(sellerId, id);
    }

    public async Task<Storage> CreateStorageAsync(CreateStorageRequest request, int sellerId)
    {
        return await _repo.CreateStorageAsync(request, sellerId);
    }

    public async Task<Storage?> UpdateStorageAsync(int sellerId, int id, CreateStorageRequest request)
    {
        return await _repo.UpdateStorageAsync(sellerId, id, request);
    }

    public async Task<Storage?> DeleteStorageAsync(int sellerId, int id)
    {
        return await _repo.DeleteStorageAsync(sellerId, id);
    }
}
