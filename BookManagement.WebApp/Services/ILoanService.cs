using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.ReturnLoan;
using BookManagement.Application.Models;

namespace BookManagement.WebApp.Services;

public interface ILoanService
{
    Task<ResultViewModel<IEnumerable<LoanViewModel>>> GetAllLoans();
    Task<ResultViewModel<LoanDetailViewModel>> GetLoanById(int id);
    Task<ResultViewModel<int>> CreateLoan(CreateLoanCommand command);
    Task<ResultViewModel> ReturnLoan(int id);
}
