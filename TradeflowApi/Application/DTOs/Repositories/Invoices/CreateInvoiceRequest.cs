namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Invoices;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Users;

public class CreateInvoiceRequest
{
    public required InvoiceDTO Invoice { get; set; }
    public required SellerDTO Seller { get; set; }
}
