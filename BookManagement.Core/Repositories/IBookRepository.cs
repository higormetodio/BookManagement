using BookManagement.Core.Entities;

namespace BookManagement.Core.Repositories;
public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> GetBookLoansByIdAsync(int id);
    Task<int> CreateBookAsync(Book book);
    Task<int> UpdateBookStockAsync(BookStock stock);
    Task<int> UpdateBookAsync(Book book);
}
