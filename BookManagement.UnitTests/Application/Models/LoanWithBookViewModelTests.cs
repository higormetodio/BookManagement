using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class LoanWithBookViewModelTests
{
    private readonly FakeData _fakeData;
    private readonly ILoanRepository _loanRepository;

    public LoanWithBookViewModelTests()
    {
        _fakeData = new FakeData();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Create_Loan_With_Book_View_Model_With_Correct_Properties()
    {
        //Arrange
        var loan = await _loanRepository.GetLoanByIdAsync(1);

        //Act
        var loanWithBookViewModel = LoanWithBookViewModel.FromEntity(loan);

        //Assert
        Assert.NotNull(loanWithBookViewModel);
        Assert.Equal(loan.Id, loanWithBookViewModel.LoanId);
        Assert.Equal(loan.Book.Title, loanWithBookViewModel.BookTitle);
        Assert.Equal(loan.LoanDate, loanWithBookViewModel.LoanDate);
        Assert.Equal(loan.ReturnDate, loanWithBookViewModel.ReturnDate);
        Assert.Equal(loan.Status.ToString(), loanWithBookViewModel.Status);
    }
}
