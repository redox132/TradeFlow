using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.DTOs.Auth;
using Tradeflow.Application.Interfaces.Auth;

namespace Tradeflow.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public LoginController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // TODO: validate user via DB
        var token = _tokenService.GenerateToken(1, request.Email);
        return Ok(new { token });
    }
}
