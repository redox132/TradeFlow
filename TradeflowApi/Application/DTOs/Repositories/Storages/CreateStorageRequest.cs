namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Storages;

public class CreateStorageRequest
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int Capacity { get; set; }
}
