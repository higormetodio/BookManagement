using BookManagement.Application.Queries.GetBookLoans;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetBookLoansQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public GetBookLoansQueryTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Not_Return_Loans_As_BookLoansViewModel_Given_Unregistred_BookId()
    {
        //Arrange
        var bookGetBookLoanQuery = new GetBookLoansQuery(10);
        var bookGetBookLoanHandler = new GetBookLoansHandler(_bookRepository);

        //Act
        var bookResultViewModel = await bookGetBookLoanHandler.Handle(bookGetBookLoanQuery, new CancellationToken());

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.False(bookResultViewModel.IsSuccess);
        Assert.Equal("Book not found", bookResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Loans_As_BookLoansViewModel_Given_Inactive_BookId()
    {
        //Arrange
        var bookGetBookLoanQuery = new GetBookLoansQuery(4);
        var bookGetBookLoanHandler = new GetBookLoansHandler(_bookRepository);

        //Act
        var bookResultViewModel = await bookGetBookLoanHandler.Handle(bookGetBookLoanQuery, new CancellationToken());

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.False(bookResultViewModel.IsSuccess);
        Assert.Equal("Book not found", bookResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_Two_Loans_As_BookLoansViewModel_Given_BookId()
    {
        //Arrange
        var bookGetBookLoanQuery = new GetBookLoansQuery(2);
        var bookGetBookLoanHandler = new GetBookLoansHandler(_bookRepository);
        var book = await _bookRepository.GetBookLoansByIdAsync(2);

        //Act
        var bookResultViewModel = await bookGetBookLoanHandler.Handle(bookGetBookLoanQuery, new CancellationToken());
        var bookLoansViewModel = bookResultViewModel.Data;

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookLoansViewModel);
        Assert.Equal(book.Title, bookLoansViewModel.Title);
        Assert.Equal(2, bookLoansViewModel.Loans.Count());
    }

    [Fact]
    public async Task Should_Return_One_Loan_As_BookLoansViewModel_Given_BookId()
    {
        //Arrange
        var bookGetBookLoanQuery = new GetBookLoansQuery(1);
        var bookGetBookLoanHandler = new GetBookLoansHandler(_bookRepository);
        var book = await _bookRepository.GetBookLoansByIdAsync(1);

        //Act
        var bookResultViewModel = await bookGetBookLoanHandler.Handle(bookGetBookLoanQuery, new CancellationToken());
        var bookLoansViewModel = bookResultViewModel.Data;

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookLoansViewModel);
        Assert.Equal(book.Title, bookLoansViewModel.Title);
        Assert.Equal(1, bookLoansViewModel.Loans.Count());
    }

    [Fact]
    public async Task Should_Return_Loans_With_Two_Users_AsBookLoansViewModel_Given_BookId()
    {
        //Arrange
        var bookGetBookLoanQuery = new GetBookLoansQuery(2);
        var bookGetBookLoanHandler = new GetBookLoansHandler(_bookRepository);

        //Act
        var bookResultViewModel = await bookGetBookLoanHandler.Handle(bookGetBookLoanQuery, new CancellationToken());
        var bookLoansViewModel = bookResultViewModel.Data;
        var bookLoansWithUser = bookLoansViewModel.Loans.OrderBy(u => u.UserName).ToList();

        //Assert
        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookLoansViewModel);
        Assert.Equal("Andre Silva", bookLoansWithUser[0].UserName);
        Assert.Equal("Bruno Carvalho", bookLoansWithUser[1].UserName);
    }
}
