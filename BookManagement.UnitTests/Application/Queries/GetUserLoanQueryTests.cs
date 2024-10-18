using BookManagement.Application.Queries.GetBookLoans;
using BookManagement.Application.Queries.GetUserLoans;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetUserLoanQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public GetUserLoanQueryTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Not_Return_Loans_As_UserLoansViewModel_Given_Unregistred_UserkId()
    {
        //Arrange
        var userGetUserLoanQuery = new GetUserLoansQuery(10);
        var userGetUserLoanHandler = new GetUserLoansHandler(_userRepository);

        //Act
        var userResultViewModel = await userGetUserLoanHandler.Handle(userGetUserLoanQuery, new CancellationToken());

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.False(userResultViewModel.IsSuccess);
        Assert.Equal("User not found.", userResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Loans_As_UserLoansViewModel_Given_Inactive_UserId()
    {
        //Arrange
        var userGetUserLoanQuery = new GetUserLoansQuery(4);
        var userGetUserLoanHandler = new GetUserLoansHandler(_userRepository);

        //Act
        var userResultViewModel = await userGetUserLoanHandler.Handle(userGetUserLoanQuery, new CancellationToken());

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.False(userResultViewModel.IsSuccess);
        Assert.Equal("User not found.", userResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_Three_Loans_As_UserLoansViewModel_Given_UserId()
    {
        //Arrange
        var userGetUserLoanQuery = new GetUserLoansQuery(2);
        var userGetUserLoanHandler = new GetUserLoansHandler(_userRepository);
        var user = await _userRepository.GetUserLoansByIdAsync(2);

        //Act
        var userResultViewModel = await userGetUserLoanHandler.Handle(userGetUserLoanQuery, new CancellationToken());
        var userLoansViewModel = userResultViewModel.Data;

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.True(userResultViewModel.IsSuccess);
        Assert.NotNull(userLoansViewModel);
        Assert.Equal(user.Name, userLoansViewModel.Name);
        Assert.Equal(3, userLoansViewModel.Loans.Count());
    }

    [Fact]
    public async Task Should_Return_One_Loan_As_UserLoansViewModel_Given_UserId()
    {
        //Arrange
        var userGetUserLoanQuery = new GetUserLoansQuery(3);
        var userGetUserLoanHandler = new GetUserLoansHandler(_userRepository);
        var user = await _userRepository.GetUserLoansByIdAsync(3);

        //Act
        var userResultViewModel = await userGetUserLoanHandler.Handle(userGetUserLoanQuery, new CancellationToken());
        var userLoansViewModel = userResultViewModel.Data;

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.True(userResultViewModel.IsSuccess);
        Assert.NotNull(userLoansViewModel);
        Assert.Equal(user.Name, userLoansViewModel.Name);
        Assert.Equal(1, userLoansViewModel.Loans.Count());
    }

    [Fact]
    public async Task Should_Return_Loans_With_Two_Books_As_UserLoansViewModel_Given_UserId()
    {
        //Arrange
        var userGetUserLoanQuery = new GetUserLoansQuery(1);
        var userGetUserLoanHandler = new GetUserLoansHandler(_userRepository);

        //Act
        var userResultViewModel = await userGetUserLoanHandler.Handle(userGetUserLoanQuery, new CancellationToken());
        var userLoansViewModel = userResultViewModel.Data;
        var userLoansWithBook = userLoansViewModel.Loans.OrderBy(u => u.BookTitle).ToList();

        //Assert
        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.NotNull(userLoansViewModel);
        Assert.Equal("Algoritmos - Teoria e Prática", userLoansWithBook[0].BookTitle);
        Assert.Equal("Arquitetura e Organização de Computadores", userLoansWithBook[1].BookTitle);
    }
}
