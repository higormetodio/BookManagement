using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class LoanViewModelTests
{
    private readonly FakeData _fakeData;
    private readonly ILoanRepository _loanRepository;

    public LoanViewModelTests()
    {
        _fakeData = new FakeData();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Create_Loan_View_Model_With_Correct_Properties()
    {
        //Arrange
        var loan = await _loanRepository.GetLoanByIdAsync(1);

        //Act
        var loanViewModel = LoanViewModel.FromEntity(loan);

        //Assert
        Assert.NotNull(loanViewModel);
        Assert.Equal(loan.Id, loanViewModel.Id);
        Assert.Equal(loan.User.Name, loanViewModel.UserName);
        Assert.Equal(loan.Book.Title, loanViewModel.BookTitle);
        Assert.Equal(loan.LoanDate.ToString("MM-dd-yyyy"), loanViewModel.LoanDate);
        Assert.Equal(loan.ReturnDate.ToString("MM-dd-yyyy"), loanViewModel.ReturnDate);
        Assert.Equal(loan.Status.ToString(), loanViewModel.Status);
    }
}
