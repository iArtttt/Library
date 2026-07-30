using Library.Common.DTOs;

namespace Library.Common.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateSalt();

        string HashPassword(string password, string salt);

        Task<UserDto?> AuthenticateAsync(UserLoginDto loginDto);
    }
}
