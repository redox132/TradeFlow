namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;

public class CreateOrderItemRequest
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
