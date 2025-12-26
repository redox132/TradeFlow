namespace Tradeflow.TradeflowApi.Domain.Entities;

public class Invoice
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Number { get; set; }
    public DateTime CreatedAt { get; set; }
}