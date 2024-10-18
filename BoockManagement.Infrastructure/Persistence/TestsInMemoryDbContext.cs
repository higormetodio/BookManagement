using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence;
public class TestsInMemoryDbContext
{
    public BookManagementDbContext CreateTestsDbContext()
    {
        var options = new DbContextOptionsBuilder<BookManagementDbContext>().UseInMemoryDatabase("TestesInMemoryDatabase").Options;

        var testsDbContext = new BookManagementDbContext(options);

        return testsDbContext;                                
    }
}
