namespace Tradeflow.TradeflowApi.Domain.Entities;

using Tradeflow.TradeflowApi.Domain.Entities;

public class ApiKey
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public bool IsRevoked { get; set; }

    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
}
