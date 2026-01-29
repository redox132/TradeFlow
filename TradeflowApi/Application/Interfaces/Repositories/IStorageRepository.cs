using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Storages;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

public interface IStorageRepository
{
    Task<List<StorageDTO>> GetStoragesAsync(int sellerId, int pageNumber, int pageSize);
    Task<StorageDTO?> GetStorageByIdAsync(int sellerId, int id);
    Task<Storage> CreateStorageAsync(CreateStorageRequest request, int sellerId);
    Task<Storage?> UpdateStorageAsync(int sellerId, int id, CreateStorageRequest request);
    Task<Storage?> DeleteStorageAsync(int sellerId, int id);
}
