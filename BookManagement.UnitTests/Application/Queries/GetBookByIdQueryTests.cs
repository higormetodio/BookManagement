using BookManagement.Application.Queries.GetBookById;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetBookByIdQueryTests
{
    private readonly IBookRepository _bookRepository;

    public GetBookByIdQueryTests()
    {
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Not_Return_One_Book_As_BookDetailViewModel_Given_Unregistred_Id()
    {
        //Arrange
        var getBookByIdQuery = new GetBookByIdQuery(10);
        var getBookByIdHandler = new GetBookByIdHandler(_bookRepository);

        //Act
        var bookResultViewModel = await getBookByIdHandler.Handle(getBookByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.False(bookResultViewModel.IsSuccess);
        Assert.Equal("Book not found", bookResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Book_As_BookDetailViewModel_With_Inactive_Status_Given_Id()
    {
        //Arrange
        var getBookByIdQuery = new GetBookByIdQuery(4);
        var getBookByIdHandler = new GetBookByIdHandler(_bookRepository);

        //Act
        var bookResultViewModel = await getBookByIdHandler.Handle(getBookByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.False(bookResultViewModel.IsSuccess);
        Assert.Equal("Book not found", bookResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_One_Active_Book_As_BookDetailViewModel_Given_Id()
    {
        //Arrange
        var getBookByIdQuery = new GetBookByIdQuery(1);
        var getBookByIdHandler = new GetBookByIdHandler(_bookRepository);
        var book = await _bookRepository.GetBookByIdAsync(1);

        //Act
        var bookResultViewModel = await getBookByIdHandler.Handle(getBookByIdQuery, new CancellationToken());
        var bookDetailViewModel = bookResultViewModel.Data;

        //Assert
        Assert.NotNull(bookResultViewModel);
        Assert.True(bookResultViewModel.IsSuccess);
        Assert.NotNull(bookDetailViewModel);
        Assert.Equal(book.Title, bookDetailViewModel.Title);
    }
}
