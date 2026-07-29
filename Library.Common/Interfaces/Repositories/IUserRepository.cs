using Library.Common.DTOs;
using Library.Common.Entities;

namespace Library.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> FindUserAsync(string? toSearch);
        Task<User?> GetByIdAsync(Guid id);

        Task AddAsync(UserRegisterDto user);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(User user);
    }
}
