using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence;
public class BookManagementDbContext : DbContext
{
    public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options) : base(options)
    { }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<BookStock> BookStocks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(BookManagementDbContext).Assembly);
    }

}
