namespace LatestEcommAPI.Models;

public class Address
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
}