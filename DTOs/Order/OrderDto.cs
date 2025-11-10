using LatestEcommAPI.DTOs.ShipmentDetails;

namespace LatestEcommAPI.DTOs.Order
{
    public class OrderDto
    {
        public required int OrderId { get; set; }
        public required int ProductID { get; set; }
        public required int Quantity { get; set; }
        public required DateTime OrderDate { get; set; }

        // Using object? for now as you requested
        public ShipmentDetailsDto? ShipmentDetails { get; set; }
    }
}
