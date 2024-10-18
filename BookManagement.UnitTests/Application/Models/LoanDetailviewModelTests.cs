using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class LoanDetailviewModelTests
{
    private readonly FakeData _fakeData;
    private readonly ILoanRepository _loanRepository;

    public LoanDetailviewModelTests()
    {
        _fakeData = new FakeData();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Create_Loan_Detail_View_Model_With_Correct_Properties()
    {
        //Arrange
        var loan = await _loanRepository.GetLoanByIdAsync(1);

        //Act
        var loanDetailViewModel = LoanDetailViewModel.FromEntity(loan);

        //Assert
        Assert.NotNull(loanDetailViewModel);
        Assert.Equal(loan.Id, loanDetailViewModel.Id);
        Assert.Equal(loan.UserId, loanDetailViewModel.UserId);
        Assert.Equal(loan.User.Name, loanDetailViewModel.UserName);
        Assert.Equal(loan.BookId, loanDetailViewModel.BookId);
        Assert.Equal(loan.Book.Title, loanDetailViewModel.BookTitle);
        Assert.Equal(loan.LoanDate.ToString("MM-dd-yyyy"), loanDetailViewModel.LoanDate);
        Assert.Equal(loan.ReturnDate.ToString("MM-dd-yyyy"), loanDetailViewModel.ReturnDate);
        Assert.Equal(loan.Status.ToString(), loanDetailViewModel.Status);
    }
}
