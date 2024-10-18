using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
public class FakeLoanRepository : ILoanRepository
{
    protected readonly BookManagementDbContext _dbContext;

    public FakeLoanRepository()
    {
        var testInMemoryDbContext = new TestsInMemoryDbContext();
        _dbContext = testInMemoryDbContext.CreateTestsDbContext();
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return await _dbContext.Loans.AsNoTracking()
                               .Include(l => l.User)
                               .Include(l => l.Book)
                               .ToListAsync();
    }

    public async Task<Loan> GetLoanByIdAsync(int id)
    {
        return await _dbContext.Loans.Include(l => l.User)
                                       .Include(l => l.Book)
                                       .Include(l => l.User)
                                       .SingleOrDefaultAsync(l => l.Id == id);
    }

    public async Task<int> CreateLoanAsync(Loan loan)
    {
        await _dbContext.Loans.AddAsync(loan);
        await _dbContext.SaveChangesAsync();

        return loan.Id;
    }

    public async Task<int> UpdateLoanAsync(Loan loan)
    {
        _dbContext.Loans.Update(loan);
        await _dbContext.SaveChangesAsync();

        return loan.Id;
    }
}
