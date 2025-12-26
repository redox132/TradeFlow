namespace Tradeflow.TradeflowApi.Domain.Entities;

using Tradeflow.TradeflowApi.Domain.Entities;

public class Address
{
    public int Id { set; get; }
    public int UserId { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? FlatNumber { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public Country? Country { get; set; }
}