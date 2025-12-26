namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;

public class GetInvoiceRequest
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public required string Number { get; set; }
}
