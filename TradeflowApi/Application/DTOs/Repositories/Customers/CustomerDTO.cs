namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Addresses;

public class CustomerDTO
{
    public int Id { get; set; } 
    public string? FName { get; set; }
    public string? LName { get; set; }
    public string? Email { get; set; }
    public AddressDTO? AddressDTO { get; set; }
}