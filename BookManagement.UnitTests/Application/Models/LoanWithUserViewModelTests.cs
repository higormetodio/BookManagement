using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class LoanWithUserViewModelTests
{
    private readonly FakeData _fakeData;
    private readonly ILoanRepository _loanRepository;
    public LoanWithUserViewModelTests()
    {
        _fakeData = new FakeData();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Create_Loan_With_User_View_Model_With_Correct_Properties()
    {
        //Arrange
        var loan = await _loanRepository.GetLoanByIdAsync(1);

        //Act
        var loanWithUserViewModel = LoanWithUserViewModel.FromEntity(loan);

        //Assert
        Assert.NotNull(loanWithUserViewModel);
        Assert.Equal(loan.Id, loanWithUserViewModel.LoanId);
        Assert.Equal(loan.User.Name, loanWithUserViewModel.UserName);
        Assert.Equal(loan.LoanDate.ToString("MM-dd-yyyy"), loanWithUserViewModel.LoanDate);
        Assert.Equal(loan.ReturnDate.ToString("MM-dd-yyyy"), loanWithUserViewModel.ReturnDate);
        Assert.Equal(loan.Status.ToString(), loanWithUserViewModel.Status);
    }
}
