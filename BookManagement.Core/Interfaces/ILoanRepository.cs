using BookManagement.Core.Entities;

namespace BookManagement.Core.Interfaces;
public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<Loan> GetLoanByIdAsync(int id);
    Task CreateLoanAsync(Loan loan);
    Task UpdateLoanAsync(Loan loan);
    Task CancelLoanAsync(Loan loan);
}
