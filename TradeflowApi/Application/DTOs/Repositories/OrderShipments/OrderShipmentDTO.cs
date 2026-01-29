namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderShipments;

public class OrderShipmentDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public string Carrier { get; set; } = null!;
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string Status { get; set; } = null!;
    public int SellerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
