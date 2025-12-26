namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Domain.Enums;


public class CreateOrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}