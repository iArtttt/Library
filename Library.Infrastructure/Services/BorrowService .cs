using Library.Common.DTOs;
using Library.Common.Entities;
using Library.Common.Enums;
using Library.Common.Interfaces.Repositories;
using Library.Common.Interfaces.Services;

namespace Library.Infrastructure.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBorrowedBookRepository _borrowRepository;

        public BorrowService(IBookRepository bookRepository, IUserRepository userRepository, IBorrowedBookRepository borrowRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _borrowRepository = borrowRepository;
        }

        public async Task<bool> BorrowBookAsync(Guid readerId, Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null || book.Count <= 0) return false;

            var user = await _userRepository.GetByIdAsync(readerId);
            if (user == null || user.Role != Role.Reader) return false;

            var loan = new BorrowedBook
            {
                Id = Guid.NewGuid(),
                BookId = book.Id,
                ReaderId = user.Id,
                Taken = DateTime.Now,
                ToReturn = DateTime.Now.AddDays(book.ReturnedDays),
                IsReturned = false
            };

            book.Count--;

            await _borrowRepository.AddAsync(loan);
            await _bookRepository.UpdateAsync(book);
            return true;
        }

        public async Task<bool> ReturnBookAsync(Guid loanId)
        {
            var loan = await _borrowRepository.GetActiveLoanAsync(loanId);
            if (loan == null || loan.IsReturned) return false;

            loan.IsReturned = true;

            if (loan.Book != null)
            {
                loan.Book.Count++;
                await _bookRepository.UpdateAsync(loan.Book);
            }

            await _borrowRepository.UpdateAsync(loan);
            return true;
        }
        public async Task<List<BorrowedBookDto>> GetReaderHistoryAsync(Guid readerId)
        {
            // 1. Fetch raw tracking history from database via decoupled repository
            var rawHistory = await _borrowRepository.GetReaderHistoryAsync(readerId);

            // 2. Perform business calculations and map to clean immutable DTO records
            return rawHistory
                .OrderBy(l => l.IsReturned) 
                .ThenByDescending(l => l.Taken) 
                .Select(l =>
                {
                    bool isOverdue = !l.IsReturned && l.ToReturn < DateTime.Now;
                    int overdueDays = isOverdue ? (DateTime.Now - l.ToReturn).Days : 0;

                    return new BorrowedBookDto(
                        l.Id,
                        l.BookId,
                        l.Book?.Name ?? "Unknown Book",
                        l.Taken.ToShortDateString(),
                        l.ToReturn.ToShortDateString(),
                        l.IsReturned,
                        isOverdue,
                        overdueDays
                    );
                })
                .ToList();
        }
    }
}
