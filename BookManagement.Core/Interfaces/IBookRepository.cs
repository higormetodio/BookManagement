using BookManagement.Core.Entities;

namespace BookManagement.Core.Interfaces;
public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> GetBookLoansByIdAsync(int id);
    Task<int> CreateBookAsync(Book book);
    Task UpdateBookStockAsync(BookStock stock);
    Task UpdateBookAsync(Book book);
}
