using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.DTOs;
using Tradeflow.Application.Interfaces;

namespace Tradeflow.Api.Controllers.Auth;

[ApiController]
[Route("api/test")]
public class LoginController : ControllerBase
{
   
    // GET api/test?name=John&age=25
    [HttpGet]
    public IActionResult Get([FromQuery] TestDTO testDTO)
    {
        // You can use the DTO here
        return Ok(new { message = $"Hello {testDTO.Name}" });
    }

}
