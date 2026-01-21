using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        // Get seller from middleware (populated by ApiKeyMiddleware using X-API-KEY)
        if (!HttpContext.Items.TryGetValue("Seller", out var sellerObj) || sellerObj is not Seller seller)
            return Unauthorized();

        var countries = await _countryService.GetCountriesAsync(seller.Id);
        return Ok(countries);
    }
}
