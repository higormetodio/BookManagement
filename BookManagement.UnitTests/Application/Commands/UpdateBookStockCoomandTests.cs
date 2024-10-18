using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class UpdateBookStockCoomandTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;

    public UpdateBookStockCoomandTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Not_Updat_Quantity_Book_Stock_Given_UpdateBookStockCommand_With_Unregistred_BookId()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 10,
            Quantity = 5
        };

        var bookUpdateBookStockHandler = new UpdateBookStockHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookStockHandler.Handle(bookUpdateBookStockCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Updat_Quantity_Book_Stock_Given_UpdateBookStockCommand_With_Inactive_BookId()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 4,
            Quantity = 5
        };

        var bookUpdateBookStockHandler = new UpdateBookStockHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookStockHandler.Handle(bookUpdateBookStockCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Update_Quantity_Book_Stock_Given_UpdateBookStockCommand()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 1,
            Quantity = 15
        };

        var bookUpdateBookStockHandler = new UpdateBookStockHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookStockHandler.Handle(bookUpdateBookStockCommand, new CancellationToken());
        var book = await _bookRepository.GetBookByIdAsync(1);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(book);
        Assert.Equal(bookUpdateBookStockCommand.Quantity, book.Stock.Quantity);
    }
}
