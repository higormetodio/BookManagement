using BookManagement.Core.Entities;

namespace BookManagement.Core.Interfaces;
public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int? id);
    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Book book);
}
