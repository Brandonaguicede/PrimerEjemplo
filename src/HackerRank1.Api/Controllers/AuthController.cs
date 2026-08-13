using HackerRank1.Application.Common;
using HackerRank1.Application.DTO;
using HackerRank1.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerRank1.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthController(IAuthenticationService _authenticationService)
    {
        authenticationService = _authenticationService;
    }

    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(User user) 
    {
        var result = await authenticationService.LoginAsync(user);
        if (result.Status == ServiceResultStatus.Unauthorized)
            return Unauthorized();

        return Ok(result.Value);
    }
}
