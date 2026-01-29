namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.ShipmentMethods;

public class ShipmentMethodDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Integration { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? FreeFrom { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
