using System.Collections.Generic;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;

namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;

public class CreateOrderRequest
{
    public int UserId { get; set; }
    public ICollection<CreateOrderItemRequest> Items { get; set; }
        = new List<CreateOrderItemRequest>();
}
