using System.Security.Claims;
using E_Commerce.Application.DTOs.Auth;
using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _authService.LoginAsync(loginDto);
        return user is null ? Unauthorized() : Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = await _authService.RegisterAsync(registerDto);
        return user is null ? BadRequest("Email is already in use or registration failed.") : Ok(user);
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
    {
        return Ok(await _authService.CheckEmailExistsAsync(email));
    }

    [Authorize]
    [HttpGet("currentuser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            return Unauthorized();
        }

        var user = await _authService.GetCurrentUserAsync(email);
        return user is null ? NotFound() : Ok(user);
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            return Unauthorized();
        }

        var address = await _authService.GetUserAddressAsync(email);
        return address is null ? NotFound() : Ok(address);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            return Unauthorized();
        }

        var address = await _authService.UpdateUserAddressAsync(email, addressDto);
        return address is null ? BadRequest() : Ok(address);
    }
}
