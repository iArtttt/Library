using Library.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Library.Common.Interfaces.Repositories;

namespace Library.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;

        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<User>> FindUserAsync(string? toSearch)
        {
            var query = _context.Users.AsNoTracking();

            if (string.IsNullOrEmpty(toSearch))
            {
                return await query.ToListAsync();
            }

            return await query.Where(u =>
                u.Name.Contains(toSearch) ||
                u.LastName.Contains(toSearch) ||
                u.Login.Contains(toSearch)
            ).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
