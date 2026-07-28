using Library.Common.Entities;
using Library.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> FindBooksAsync(string? toSearch)
        {
            var query = _context.Books.Include(b => b.Authors).AsNoTracking();

            if (string.IsNullOrEmpty(toSearch))
            {
                return await query.ToListAsync(); 
            }

            return await query.Where(b =>
                b.Name.Contains(toSearch) ||
                b.Authors.Any(a => a.Name.Contains(toSearch) || a.LastName.Contains(toSearch))
            ).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id) => await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id);

    }
}
