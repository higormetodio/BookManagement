using BookManagement.Core.Entities;
using BookManagement.Core.Interfaces;
using BookManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Repositories;
public class LoanRepository : ILoanRepository
{
    private readonly BookManagementDbContext _context;

    public LoanRepository(BookManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        => await _context.Loans.AsNoTracking()
                               .Include(l => l.User)
                               .Include(l => l.Book)
                               .ToListAsync();

    public async Task<Loan> GetLoanByIdAsync(int id)
    => await _context.Loans.Include(l => l.User)
                                       .Include(l => l.Book)
                                       .Include(l => l.User)
                                       .SingleOrDefaultAsync(l => l.Id == id);
    
    public async Task CreateLoanAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLoanAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
    }

    public async Task CancelLoanAsync(Loan loan)
    {
        throw new NotImplementedException();
    }
}
