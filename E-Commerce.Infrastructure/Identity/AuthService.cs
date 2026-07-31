using E_Commerce.Application.DTOs.Auth;
using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<UserDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user is null)
        {
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        return result.Succeeded ? await CreateUserDtoAsync(user) : null;
    }

    public async Task<UserDto?> RegisterAsync(RegisterDto registerDto)
    {
        if (await CheckEmailExistsAsync(registerDto.Email))
        {
            return null;
        }

        var user = new ApplicationUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            Address = MapAddress(registerDto.Address)
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return null;
        }

        await _userManager.AddToRoleAsync(user, "Customer");
        return await CreateUserDtoAsync(user);
    }

    public Task<bool> CheckEmailExistsAsync(string email)
    {
        return _userManager.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<UserDto?> GetCurrentUserAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user is null ? null : await CreateUserDtoAsync(user);
    }

    public async Task<AddressDto?> GetUserAddressAsync(string email)
    {
        var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
        return user?.Address is null ? null : MapAddressDto(user.Address);
    }

    public async Task<AddressDto?> UpdateUserAddressAsync(string email, AddressDto addressDto)
    {
        var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
        if (user is null)
        {
            return null;
        }

        user.Address = MapAddress(addressDto);
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded ? MapAddressDto(user.Address) : null;
    }

    private async Task<UserDto> CreateUserDtoAsync(ApplicationUser user)
    {
        return new UserDto
        {
            Email = user.Email ?? string.Empty,
            DisplayName = user.DisplayName,
            Token = await _tokenService.CreateTokenAsync(user)
        };
    }

    private static Address MapAddress(AddressDto addressDto)
    {
        return new Address
        {
            FirstName = addressDto.FirstName,
            LastName = addressDto.LastName,
            Street = addressDto.Street,
            City = addressDto.City,
            Country = addressDto.Country
        };
    }

    private static AddressDto MapAddressDto(Address address)
    {
        return new AddressDto
        {
            FirstName = address.FirstName,
            LastName = address.LastName,
            Street = address.Street,
            City = address.City,
            Country = address.Country
        };
    }
}
