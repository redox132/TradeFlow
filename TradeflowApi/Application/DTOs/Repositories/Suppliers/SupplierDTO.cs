namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Suppliers;

public class SupplierDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? Phone { get; set; }
}
