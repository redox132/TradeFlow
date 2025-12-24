using Microsoft.AspNetCore.Mvc;
using Tradeflow.TradeflowApi.Application.DTOs.Auth;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;
using Tradeflow.TradeflowApi.Application.Services.Auth;

namespace Tradeflow.TradeflowApi.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ILoginService _loginService;

    public LoginController(ITokenService tokenService, ILoginService loginService)
    {
        _tokenService = tokenService;
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var token = _loginService.LoginAsync(
            request.Email,
            request.Password
        );

        return Ok(new { token });
    }
}
