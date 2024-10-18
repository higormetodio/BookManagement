using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class UserLoansViewModelTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public UserLoansViewModelTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Create_User_Loans_View_Model_And_Loan_With_Book_ViewModel_With_Correct_Properties()
    {
        //Arrange
        var user = await _userRepository.GetUserLoansByIdAsync(1);
        var loans = user.Loans;
        var loanWithBookViewModel = loans.Select(LoanWithBookViewModel.FromEntity);

        //Act
        var userLoansViewModel = new UserLoansViewModel(user.Id, user.Name, user.Email, loanWithBookViewModel);

        //Assert
        Assert.NotNull(userLoansViewModel);
        Assert.Equal(user.Id, userLoansViewModel.Id);
        Assert.Equal(user.Name, userLoansViewModel.Name);
        Assert.Equal(user.Email, userLoansViewModel.Email);
        Assert.Equal(user.Loans.Count(), userLoansViewModel.Loans.Count());
    }
}
