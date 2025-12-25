namespace Tradeflow.TradeflowApi.Domain.Entities;

public class ShipmentMethod
{
    public int Id { get; set; }

    // Display name shown to users
    public string Title { get; set; } = null!;

    // Integration identifier (e.g. "sendegoinpost", "dhl")
    public string Integration { get; set; } = null!;

    // Flat shipping price
    public decimal Price { get; set; }

    // Free shipping threshold (null = no free shipping)
    public decimal? FreeFrom { get; set; }

    // Whether this method can be used
    public bool IsActive { get; set; } = true;

    // Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
