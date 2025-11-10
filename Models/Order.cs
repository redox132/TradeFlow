namespace LatestEcommAPI.Models;

public class Order
{
    public int UserId { get; set; }
    public List<OrderItem> Orders { get; set; } = new();
    public object? Shipper { get; set; }
    public object? ShipmentDetails { get; set; }
}