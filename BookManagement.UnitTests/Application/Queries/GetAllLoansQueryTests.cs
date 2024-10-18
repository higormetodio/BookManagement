using BookManagement.Application.Queries.GetAllLoans;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetAllLoansQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public GetAllLoansQueryTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }

    [Fact]
    public async Task Should_Not_Return_Loans_As_LoanViewModel_Given_Unregistred_BookTitle()
    {
        //Arrange
        var loanGetAllLoansQuery = new GetAllLoansQuery("Sistemas Operacionais Modernos");
        var loanGetAllLoansHandler = new GetAllLoansHandler(_loanRepository);

        //Act
        var loanResultViewModel = await loanGetAllLoansHandler.Handle(loanGetAllLoansQuery, new CancellationToken());

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.False(loanResultViewModel.IsSuccess);
        Assert.Equal("Loans not found.", loanResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Loans_As_LoanViewModel_Given_Status_With_Returned()
    {
        //Arrange
        var loanGetAllLoansQuery = new GetAllLoansQuery("Compiladores: Princípios, Técnicas e Ferramentas");
        var loanGetAllLoansHandler = new GetAllLoansHandler(_loanRepository);

        //Act
        var loanResultViewModel = await loanGetAllLoansHandler.Handle(loanGetAllLoansQuery, new CancellationToken());

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.False(loanResultViewModel.IsSuccess);
        Assert.Equal("Loans not found.", loanResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_Five_Loans_As_LoanViewModel()
    {
        //Arrange
        var loanGetAllLoansQuery = new GetAllLoansQuery("");
        var loanGetAllLoansHandler = new GetAllLoansHandler(_loanRepository);
        var loans = await _loanRepository.GetAllLoansAsync();

        //Act
        var loanResultViewModel = await loanGetAllLoansHandler.Handle(loanGetAllLoansQuery, new CancellationToken());
        var loanViewModelList = loanResultViewModel.Data.ToList();

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.True(loanResultViewModel.IsSuccess);
        Assert.NotEmpty(loanViewModelList);
        Assert.Equal(5, loanViewModelList.Count());
    }

    [Fact]
    public async Task Should_Return_Two_Loans_As_LoanViewModel_Given_BookTitle()
    {
        //Arrange
        var loanGetAllLoansQuery = new GetAllLoansQuery("Redes de Computadores");
        var loanGetAllLoansHandler = new GetAllLoansHandler(_loanRepository);

        //Act
        var loanResultViewModel = await loanGetAllLoansHandler.Handle(loanGetAllLoansQuery, new CancellationToken());
        var loanViewModelList = loanResultViewModel.Data.ToList();

        //Assert
        Assert.NotNull(loanResultViewModel);
        Assert.True(loanResultViewModel.IsSuccess);
        Assert.NotEmpty(loanViewModelList);
        Assert.Equal(2, loanViewModelList.Count());
        Assert.Equal("Redes de Computadores", loanViewModelList[0].BookTitle);
        Assert.Equal("Redes de Computadores", loanViewModelList[1].BookTitle);
    }
}
