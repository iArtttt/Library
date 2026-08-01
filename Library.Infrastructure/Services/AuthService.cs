using Library.Common.DTOs;
using Library.Common.Interfaces.Repositories;
using Library.Common.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToHexString(saltBytes);
        }

        public string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;

            string combinedInput = password + salt;

            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedInput));

            return Convert.ToHexString(bytes);
        }

        public async Task<UserDto?> AuthenticateAsync(UserLoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Login))
                return null;

            var foundUsers = await _userRepository.FindUserAsync(loginDto.Login);
            var user = foundUsers.FirstOrDefault(u => u.Login.Equals(loginDto.Login, StringComparison.OrdinalIgnoreCase));

            if (user == null) return null;

            // 🎯 SECURITY CHECK: Hash the input attempt using the user's REAL salt from DB [1]
            string inputHash = HashPassword(loginDto.Password, user.PasswordSalt);

            if (!user.PasswordHash.Equals(inputHash, StringComparison.Ordinal))
                return null;

            return new UserDto(
                user.Id, user.Login, user.Name, user.LastName,
                user.Email, user.Role, user.DocumentNumber, user.DocumentType,
                user.Birthday?.ToShortDateString()
            );
        }
    }
}
