using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
public class FakeBookRepository : IBookRepository
{
    protected readonly BookManagementDbContext _dbContext;

    public FakeBookRepository()
    {
        var testInMemoryDbContext = new TestsInMemoryDbContext();
        _dbContext = testInMemoryDbContext.CreateTestsDbContext();
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var books = await _dbContext.Books.AsNoTracking()
                                        .Include(s => s.Stock)
                                        .ToListAsync();

        return books;
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _dbContext.Books.Include(s => s.Stock)
                                 .SingleOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> GetBookLoansByIdAsync(int id)
    {
        return await _dbContext.Books.AsNoTracking()
                                 .Include(b => b.Loans)
                                 .ThenInclude(l => l.User)
                                 .Include(b => b.Stock)
                                 .SingleOrDefaultAsync(b => b.Id == id);
    }

    public async Task<int> CreateBookAsync(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();

        return book.Id;
    }

    public async Task<int> UpdateBookStockAsync(BookStock stock)
    {
        _dbContext.BookStocks.Update(stock);
        await _dbContext.SaveChangesAsync();

        return stock.BookId;
    }

    public async Task<int> UpdateBookAsync(Book book)
    {
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();

        return book.Id;
    }
}
