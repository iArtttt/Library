namespace Library.Common.DTOs
{
    public record BorrowedBookDto(
        Guid LoanId,
        Guid BookId,
        string BookName,
        string TakenDate,
        string DeadlineDate,
        bool IsReturned,
        bool IsOverdue,
        int OverdueDays
    );
}
