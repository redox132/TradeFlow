namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Addresses;

public class CustomerDTO
{
    public int Id { get; set; } 
    public string? First_name { get; set; }
    public string? Last_name { get; set; }
    public string? Email { get; set; }
    public AddressDTO? Address_detaills { get; set; }
}