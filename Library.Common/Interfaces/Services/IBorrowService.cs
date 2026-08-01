using Library.Common.DTOs;

namespace Library.Common.Interfaces.Services
{
    public interface IBorrowService
    {
        Task<bool> BorrowBookAsync(Guid readerId, Guid bookId);
        Task<bool> ReturnBookAsync(Guid loanId);
        Task<List<BorrowedBookDto>> GetReaderHistoryAsync(Guid readerId);
    }
}
