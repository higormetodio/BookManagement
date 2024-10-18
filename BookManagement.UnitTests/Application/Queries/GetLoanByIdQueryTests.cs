using BookManagement.Application.Queries.GetLoanById;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using System;

namespace BookManagement.UnitTests.Application.Queries;
public class GetLoanByIdQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public GetLoanByIdQueryTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Not_Return_Loan_As_LoanDetailViewModel_Given_Unregistred_Id()
    {
        // Arrange
        var loanGetLoanByIdQuery = new GetLoanByIdQuery(10);
        var loanGetLoanByIdHandler = new GetLoanByIdHandler(_loanRepository);

        //Act
        var loanResultViewModel = await loanGetLoanByIdHandler.Handle(loanGetLoanByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.False(loanResultViewModel.IsSuccess);
        Assert.Equal("Loan not found.", loanResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Loan_As_LoanDetailViewModel_With_Status_Returned()
    {
        // Arrange
        var loanGetLoanByIdQuery = new GetLoanByIdQuery(4);
        var loanGetLoanByIdHandler = new GetLoanByIdHandler(_loanRepository);

        //Act
        var loanResultViewModel = await loanGetLoanByIdHandler.Handle(loanGetLoanByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.False(loanResultViewModel.IsSuccess);
        Assert.Equal("Loan not found.", loanResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_One_Loan_As_LoanDetailViewModel_Given_Id()
    {
        //Arrange
        var loanGetLoanByIdQuery = new GetLoanByIdQuery(1);
        var loanGetLoanByIdHandler = new GetLoanByIdHandler(_loanRepository);
        var loan = await _loanRepository.GetLoanByIdAsync(1);

        //Act
        var loanResultViewModel = await loanGetLoanByIdHandler.Handle(loanGetLoanByIdQuery, new CancellationToken());
        var loanDetailViewModel = loanResultViewModel.Data;

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.True(loanResultViewModel.IsSuccess);
        Assert.NotNull(loanDetailViewModel);
        Assert.Equal(loan.Id, loanDetailViewModel.Id);
    }
}
