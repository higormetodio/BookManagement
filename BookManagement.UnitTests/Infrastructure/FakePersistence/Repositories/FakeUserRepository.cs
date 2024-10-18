using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
public class FakeUserRepository : IUserRepository
{
    protected readonly BookManagementDbContext _dbContext;

    public FakeUserRepository()
    {
        var testInMemoryDbContext = new TestsInMemoryDbContext();
        _dbContext = testInMemoryDbContext.CreateTestsDbContext();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.AsNoTracking()
                                 .ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(b => b.Id == id);
    }

    public async Task<User> GetUserLoansByIdAsync(int id)
    {
        return await _dbContext.Users.AsNoTracking()
                               .Include(u => u.Loans)
                               .ThenInclude(l => l.Book)
                               .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
    }

    public async Task<int> CreateUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user.Id;

    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
