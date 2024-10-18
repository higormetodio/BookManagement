using BookManagement.Application.Queries.GetAllBooks;
using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetAllBooksQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Not_Return_Books_As_BookViewModel_With_Inactive_Status()
    {
        //Arrange
        var getAllBooksQuery = new GetAllBooksQuery("Sistemas Operacionais Modernos");
        var getAllBooksHandler = new GetAllBooksHandler(_bookRepository);

        //Act
        var bookResultViewModel = await getAllBooksHandler.Handle(getAllBooksQuery, new CancellationToken());

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.False(bookResultViewModel.IsSuccess);
        Assert.Equal("Books with searched criteria, not found.", bookResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Books_As_BookViewModel_When_Word_Not_Found()
    {
        //Arrange
        var getAllBooksQuery = new GetAllBooksQuery("Código Limpo: Habilidades Práticas do Agile Software");
        var getAllBooksHandler = new GetAllBooksHandler(_bookRepository);

        //Act
        var bookResultViewModel = await getAllBooksHandler.Handle(getAllBooksQuery, new CancellationToken());

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.False(bookResultViewModel.IsSuccess);
        Assert.Equal("Books with searched criteria, not found.", bookResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_Four_Active_Books_As_BookViewModel()
    {
        //Arrange
        var getAllBooksQuery = new GetAllBooksQuery("");
        var getAllBooksHandler = new GetAllBooksHandler(_bookRepository);

        //Act
        var bookResultViewModel = await getAllBooksHandler.Handle(getAllBooksQuery, new CancellationToken());
        var bookViewModelList = bookResultViewModel.Data.ToList();

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookViewModelList);
        Assert.NotEmpty(bookViewModelList);
        Assert.Equal(4, bookViewModelList.Count());
    }

    [Fact]
    public async Task Should_Return_One_Active_Books_As_BookViewModel_By_Title()
    {
        //Arrange
        var getAllBooksQuery = new GetAllBooksQuery("arquitetura");
        var getAllBooksHandler = new GetAllBooksHandler(_bookRepository);
        var book = new Book("Arquitetura e Organização de Computadores", "William Stallings", "978-8543020532", 2017);

        //Act
        var bookResultViewModel = await getAllBooksHandler.Handle(getAllBooksQuery, new CancellationToken());
        var bookViewModel = bookResultViewModel.Data.SingleOrDefault();

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookViewModel);
        Assert.Equal(book.Title, bookViewModel.Title);
    }

    [Fact]
    public async Task Should_Return_Two_Active_Books_As_BookViewModel_By_Title()
    {
        //Arrange
        var getAllBooksQuery = new GetAllBooksQuery("computadores");
        var getAllBooksHandler = new GetAllBooksHandler(_bookRepository);
        var book1 = new Book("Arquitetura e Organização de Computadores", "William Stallings", "978-8543020532", 2017);
        var book2 = new Book("Redes de Computadores", "Andrew Tanenbaum", "978-8582605608", 2011);

        //Act
        var bookResultViewModel = await getAllBooksHandler.Handle(getAllBooksQuery, new CancellationToken());
        var bookViewModelList = bookResultViewModel.Data.ToList();
        var bookViewModel1 = bookResultViewModel.Data.SingleOrDefault(b => b.Id == 3);
        var bookViewModel2 = bookResultViewModel.Data.SingleOrDefault(b => b.Id == 5);

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookViewModelList);
        Assert.NotEmpty(bookViewModelList);
        Assert.Contains(book1.Title, bookViewModel1.Title);
        Assert.Contains(book2.Title, bookViewModel2.Title);
    }
}
