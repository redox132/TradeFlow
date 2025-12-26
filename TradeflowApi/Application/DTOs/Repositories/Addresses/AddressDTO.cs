namespace Tradeflow.TradeflowApi.Application.DTOs.Repositories.Addresses;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;

public class AddressDTO
{
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? FlatNumber { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public CountryDTO? CountryDTO { get; set; }
}