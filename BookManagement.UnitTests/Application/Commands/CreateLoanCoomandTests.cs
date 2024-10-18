using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class CreateLoanCoomandTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public CreateLoanCoomandTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Not_Create_Loan_As_CreateLoanCommand_Given_Unregistred_UserId()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 10,
            BookId = 1
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Create_Loan_As_CreateLoanCommand_Given_Inactive_UserId()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 4,
            BookId = 1
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Create_Loan_As_CreateLoanCommand_Given_Unregistred_BookId()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 15
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Create_Loan_As_CreateLoanCommand_Given_Inactive_BookId()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 4
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Create_Loan_As_CreateLoanCommand_Given_Already_Loaned_Book()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 2
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("This user already has a book \"Algoritmos - Teoria e Prática\" loaned", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Create_Loan_As_CreateLoanCommand_Given_Zero_Book_Stock()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 3
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("There is no book available.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Create_Loan_As_CreateLoanCoomand_Returning_Id()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 3,
            BookId = 1
        };

        var loanCreateLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);

        //Act
        var resultViewModel = await loanCreateLoanHandler.Handle(loanCreateLoanCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var loan = await _loanRepository.GetLoanByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.True(id > 0);
        Assert.NotNull(loan);
        Assert.Equal(id, loan.Id);
        Assert.Equal(loan.UserId, loanCreateLoanCommand.UserId);
        Assert.Equal(loan.BookId, loanCreateLoanCommand.BookId);
    }
}
