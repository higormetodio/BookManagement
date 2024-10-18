using BookManagement.Application.Commands.ReturnLoan;
using BookManagement.Core.Enums;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class ReturnLoanCoomandTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;

    public ReturnLoanCoomandTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Not_Update_Status_Loan_To_Returned_Given_Unregister_Loan_Id()
    {
        //Arrange
        var loanReturnLoanCommand = new ReturnLoanCommand(10);
        var loanReturnLoanHandler = new ReturnLoanHandler(_loanRepository, _bookRepository);

        //Act
        var resultViewModel = await loanReturnLoanHandler.Handle(loanReturnLoanCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Loan not found", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Update_Status_Loan_To_Returned_Given_Loan_Id_Already_Returned()
    {
        //Arrange
        var loanReturnLoanCommand = new ReturnLoanCommand(4);
        var loanReturnLoanHandler = new ReturnLoanHandler(_loanRepository, _bookRepository);

        //Act
        var resultViewModel = await loanReturnLoanHandler.Handle(loanReturnLoanCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Loan not found", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Update_Status_Loan_To_Returned_Given_Loan_Id()
    {
        //Arrange
        var loanReturnLoanCommand = new ReturnLoanCommand(1);
        var loanReturnLoanHandler = new ReturnLoanHandler(_loanRepository, _bookRepository);

        //Act
        var resultViewModel = await loanReturnLoanHandler.Handle(loanReturnLoanCommand, new CancellationToken());
        var loan = await _loanRepository.GetLoanByIdAsync(1);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(loan);
        Assert.Equal(LoanStatus.Returned, loan.Status);
    }
}
