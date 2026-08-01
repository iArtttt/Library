using Library.Common.Entities;

namespace Library.Common.Interfaces.Repositories
{
    public interface IBorrowedBookRepository
    {
        Task<BorrowedBook?> GetActiveLoanAsync(Guid loanId);
        Task<List<BorrowedBook>> GetReaderHistoryAsync(Guid readerId);
        Task AddAsync(BorrowedBook loan);
        Task UpdateAsync(BorrowedBook loan);
    }
}
