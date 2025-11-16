namespace LatestEcommAPI.Models;

public class Carrier
{
    public int Id { get; } 
    public required int UserId { get; set; } 
    public required string Name { get; set; }   
    public required string Code { get; set; }   
    public string? TracingUrl { get; set; }   
    public string? SupportsTracking { get; set; }   
    public string? SupportInternational { get; set; }   
    public string? IsActive { get; set; }   
}