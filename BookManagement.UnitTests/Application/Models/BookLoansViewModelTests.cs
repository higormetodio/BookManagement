using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class BookLoansViewModelTests
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public BookLoansViewModelTests()
    {
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Create_Book_Loans_View_Model_And_Loan_With_User_ViewModel_With_Correct_Properties()
    {
        //Arrange
        var book = await _bookRepository.GetBookLoansByIdAsync(1);
        var loans = book.Loans;
        var loanWithUserViewModel = loans.Select(LoanWithUserViewModel.FromEntity);

        //Act
        var bookLoansViewModel = new BookLoansViewModel(book.Id, book.Title, book.Stock.Quantity, book.Stock.LoanQuantity, loanWithUserViewModel);

        //Assert
        Assert.NotNull(bookLoansViewModel);
        Assert.Equal(book.Id, bookLoansViewModel.Id);
        Assert.Equal(book.Title, bookLoansViewModel.Title);
        Assert.Equal(book.Stock.Quantity, bookLoansViewModel.Quantity);
        Assert.Equal(book.Stock.LoanQuantity, bookLoansViewModel.LoanQuantity);
        Assert.Equal(book.Loans.Count(), bookLoansViewModel.Loans.Count());
    }
}
