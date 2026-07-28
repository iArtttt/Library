using Library.Common.Interfaces.DAL;

namespace Library.Common.Entities
{
    public class BorrowedBook : IID
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        
        public Book Book { get; set; } = null!;

        public Guid ReaderId { get; set; }

        public User Reader { get; set; } = null!;

        public DateTime Taken { get; set; } = DateTime.UtcNow;

        public DateTime? WhenReturned { get; set; }

        public DateTime ToReturn { get; set; }

        public bool IsReturned { get; set; }
    }
}
