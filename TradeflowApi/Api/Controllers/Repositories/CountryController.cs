using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;

namespace Tradeflow.TradeflowApi.Api.Controllers.Repositories;

[ApiController]
[Route("api")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _countryService.GetCountriesAsync();
        return Ok(countries);
    }
}
