using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.CreateLoan;
public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, ResultViewModel<int>>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;

    public CreateLoanHandler(ILoanRepository repository, IBookRepository repositoryBook, IUserRepository userRepository)
    {
        _loanRepository = repository;
        _bookRepository = repositoryBook;
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<int>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {     
        var user = await _userRepository.GetUserLoansByIdAsync(request.UserId);
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);

        if (user is null || !user.Active)
        {
            return ResultViewModel<int>.Error("User not found.");
        }

        if (book is null || !book.Active)
        {
            return ResultViewModel<int>.Error("Book not found.");
        }

        var hasBook = user.Loans.Any(l => l.BookId == book.Id);

        if (hasBook)
        {
            return ResultViewModel<int>.Error($"This user already has a book \"{book.Title}\" loaned");
        }

        if (book.Stock.Quantity <= 0)
        {
            return ResultViewModel<int>.Error("There is no book available.");
        }

        var loan = request.ToEntity();

        await _loanRepository.CreateLoanAsync(loan);        
        
        book.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book.Stock);

        return ResultViewModel<int>.Success(loan.Id);
    }
}
