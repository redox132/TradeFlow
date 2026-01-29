namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Storages;

public class StorageDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
