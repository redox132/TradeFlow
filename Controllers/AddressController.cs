using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LatestEcommAPI.Models;

namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetAddress(int id)
    {
        // Pretend we fetched an address from DB
        var address = new Address
        {
            Id = id,
            UserId = 1,
            Street = "123 Main St",
            City = "Paris",
            ZipCode = "75000",
            State = "Mazowieckie",
            Country = "Poland"
        };

        return Ok(address);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAddress([FromBody] Address address)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var body = await reader.ReadToEndAsync();
            Console.WriteLine(body);
        }
        return Ok(address);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address adress)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var body = await reader.ReadToEndAsync();
            Console.WriteLine(body);
            return Ok(adress);
        }
    }

}