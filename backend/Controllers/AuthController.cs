using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);
        if (response is null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        return Ok(response);
    }
}
