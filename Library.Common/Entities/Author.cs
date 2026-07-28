using Library.Common.Interfaces.DAL.Complex;

namespace Library.Common.Entities
{
    public class Author : IAuthor
    {
        public Guid Id {  get; set; }
        
        public string Name { get; set; } = null!;
        
        public string LastName { get; set; } = null!;
        
        public string? SecondName { get; set; }
        
        public ICollection<Book> Books { get; set; } = new List<Book>();

        IEnumerable<IBook> IAuthor.Books => Books;
    }
}
