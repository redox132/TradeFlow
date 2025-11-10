namespace LatestEcommAPI.DTOs.ShipmentDetails
{
    public class ShipmentDetailsDto
    {
        public string? Carrier { get; set; }
        public string? TrackingNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? City { get; set; }
        public string? Postcode { get; set; }
        public string? Country { get; set; }
    }
}