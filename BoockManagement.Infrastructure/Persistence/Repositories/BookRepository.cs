using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence.Repositories;
public class BookRepository : IBookRepository
{
    private readonly BookManagementDbContext _context;

    public BookRepository(BookManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var books = await _context.Books.Include(s => s.Stock)
                                        .AsNoTracking()
                                        .ToListAsync();

        return books;
    }

    public async Task<Book> GetBookByIdAsync(int id)
        => await _context.Books.Include(s => s.Stock)
                               .SingleOrDefaultAsync(b => b.Id == id);

    public async Task<Book> GetBookLoansByIdAsync(int id)
        => await _context.Books.AsNoTracking()
                               .Include(b => b.Loans)
                               .ThenInclude(l => l.User)
                               .Include(b => b.Stock)
                               .SingleOrDefaultAsync(b => b.Id == id);

    public async Task<int> CreateBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book.Id;
    }

    public async Task<int> UpdateBookStockAsync(BookStock stock)
    {
        _context.BookStocks.Update(stock);
        await _context.SaveChangesAsync();

        return stock.BookId;
    }

    public async Task<int> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return book.Id;
    }
}
