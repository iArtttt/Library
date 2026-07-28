using Library.Common.Entities;

namespace Library.Common.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> FindBooksAsync(string? toSearch);
        Task<Book?> GetByIdAsync(Guid id);

        Task AddAsync(Book book);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Book book);
    }
}
