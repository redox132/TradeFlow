namespace Tradeflow.TradeflowApi.Domain.Entities;

using Tradeflow.TradeflowApi.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? FName { get; set; }
    public string? LName { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; }
}