using Library.Common.Entities;
using Library.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private readonly LibraryContext _context;

        public BorrowedBookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<BorrowedBook?> GetActiveLoanAsync(Guid loanId)
            => await _context.BorrowedBooks.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId);

        public async Task<List<BorrowedBook>> GetReaderHistoryAsync(Guid readerId)
            => await _context.BorrowedBooks
                .Include(l => l.Book)
                .Where(l => l.ReaderId == readerId)
                .AsNoTracking()
                .ToListAsync();

        public async Task AddAsync(BorrowedBook loan)
        {
            await _context.BorrowedBooks.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowedBook loan)
        {
            _context.BorrowedBooks.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
