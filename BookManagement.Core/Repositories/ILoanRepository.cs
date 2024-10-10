using BookManagement.Core.Entities;

namespace BookManagement.Core.Repositories;
public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<Loan> GetLoanByIdAsync(int id);
    Task<int> CreateLoanAsync(Loan loan);
    Task<int> UpdateLoanAsync(Loan loan);
}
