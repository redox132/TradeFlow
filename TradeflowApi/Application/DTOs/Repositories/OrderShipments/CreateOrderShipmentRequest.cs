namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderShipments;

public class CreateOrderShipmentRequest
{
    public int OrderId { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public string Carrier { get; set; } = null!;
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string Status { get; set; } = null!;
}
