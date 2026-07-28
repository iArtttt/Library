namespace Library.Common.Entities
{
    public class Librarian : User
    {
        public ICollection<BorrowedBook> ManagedBorrows { get; set; } = new List<BorrowedBook>();

    }
}
