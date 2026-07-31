using E_Commerce.Application.DTOs.Auth;

namespace E_Commerce.Application.Services;

public interface IAuthService
{
    Task<UserDto?> LoginAsync(LoginDto loginDto);
    Task<UserDto?> RegisterAsync(RegisterDto registerDto);
    Task<bool> CheckEmailExistsAsync(string email);
    Task<UserDto?> GetCurrentUserAsync(string email);
    Task<AddressDto?> GetUserAddressAsync(string email);
    Task<AddressDto?> UpdateUserAddressAsync(string email, AddressDto addressDto);
}
