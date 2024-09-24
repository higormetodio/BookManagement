using BookManagement.Core.Entities;

namespace BookManagement.Core.Interfaces;
public interface IBookStockRepository
{
    Task<IEnumerable<BookStock>> GetAllBookStocksAsync();
    Task<BookStock> GetBookStockByBookIdAsync(int id);
    Task UpdateBookStockAsync(BookStock stock, int quantity);
}
