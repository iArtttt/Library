using Library.Common.DTOs;
using Library.Common.Entities;
using Library.Common.Interfaces.Repositories;
using Library.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Asynchronously retrieves all registered users (Safe, no passwords leaked).
        /// GET: /api/users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userRepository.FindUserAsync(null);

            // Mapping domain entities to read-only safe records
            var response = users.Select(u => new UserDto(
                u.Id,
                u.Login,
                u.Name,
                u.LastName,
                u.Email,
                u.Role,
                u.DocumentNumber,
                u.DocumentType,
                u.Birthday?.ToShortDateString()
            ));

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto loginDto)
        {
            var userProfile = await _authService.AuthenticateAsync(loginDto);

            if (userProfile == null)
            {
                return Unauthorized(new { message = "Incorrect login username or password." }); // 401 Unauthorized
            }

            return Ok(userProfile); // Returns safe 200 OK with reader profile details
        }
        
        /// <summary>
        /// Asynchronously retrieves a specific user profile by their unique GUID identifier.
        /// GET: /api/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = $"User with ID '{id}' was not found." });
            }

            var response = new UserDto(
                user.Id,
                user.Login,
                user.Name,
                user.LastName,
                user.Email,
                user.Role,
                user.DocumentNumber,
                user.DocumentType,
                user.Birthday?.ToShortDateString()
            );

            return Ok(response);
        }

        /// <summary>
        /// Asynchronously registers a new user into the system.
        /// POST: /api/users/register
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserRegisterDto registerDto)
        {
            if (registerDto == null)
                return BadRequest(new { message = "Invalid registration payload." });

            var existingUsers = await _userRepository.FindUserAsync(registerDto.Login);
            if (existingUsers.Any(u => u.Login.Equals(registerDto.Login, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict(new { message = $"The login username '{registerDto.Login}' is already taken." }); // 409 Conflict
            }

            string uniqueSalt = _authService.GenerateSalt();
            string secureHash = _authService.HashPassword(registerDto.Password, uniqueSalt);


            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Login = registerDto.Login,
                PasswordHash = secureHash, 
                PasswordSalt = uniqueSalt, 
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Role = registerDto.Role,
                DocumentNumber = registerDto.DocumentNumber,
                DocumentType = registerDto.DocumentType,
                Birthday = registerDto.Birthday
            };

            await _userRepository.AddAsync(newUser);

            var response = new UserDto(
                newUser.Id,
                newUser.Login,
                newUser.Name,
                newUser.LastName,
                newUser.Email,
                newUser.Role,
                newUser.DocumentNumber,
                newUser.DocumentType,
                newUser.Birthday?.ToShortDateString()
            );

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, response);
        }

        /// <summary>
        /// Asynchronously updates permitted profile data fields of an existing user.
        /// PUT: /api/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto updateDto)
        {
            var dbUser = await _userRepository.GetByIdAsync(id);
            if (dbUser == null)
            {
                return NotFound(new { message = $"User with ID '{id}' was not found." });
            }

            // Mapping strictly permitted fields from our specialized Update DTO
            dbUser.Name = updateDto.Name;
            dbUser.LastName = updateDto.LastName;
            dbUser.Email = updateDto.Email;
            dbUser.DocumentNumber = updateDto.DocumentNumber;
            dbUser.DocumentType = updateDto.DocumentType;
            dbUser.Birthday = updateDto.Birthday;

            await _userRepository.UpdateAsync(dbUser);
            return NoContent(); // 204 NoContent
        }

        /// <summary>
        /// Asynchronously removes a user account from the database by its GUID.
        /// DELETE: /api/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { message = $"Cannot delete. User with ID '{id}' was not found." });
            }

            await _userRepository.DeleteAsync(id);
            return Ok(new { message = $"User account '{existingUser.Login}' was successfully deleted." });
        }
    }
}
