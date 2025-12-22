using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.DTOs.Auth;
using Tradeflow.Application.Interfaces.Services.Auth;
using Tradeflow.Application.Services.Auth;
using Tradeflow.Application.DTOs.ActionResults;
using System.Threading.Tasks;

namespace Tradeflow.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _registerService;

    public RegisterController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var user = await _registerService.RegisterAsync(
            request.Name,
            request.Email,
            request.Password
        );

        OK ok = new OK
        {
            Status = 200,
            Message = "Account created successfully",
        };

        if (user != null)
            return Ok(ok);

        return NoContent();

    }
}
