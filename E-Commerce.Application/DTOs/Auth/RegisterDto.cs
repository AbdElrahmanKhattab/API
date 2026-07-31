namespace E_Commerce.Application.DTOs.Auth;

public class RegisterDto
{
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public AddressDto Address { get; set; } = new();
}
