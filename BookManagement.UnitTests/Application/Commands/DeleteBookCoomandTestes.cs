using BookManagement.Application.Commands.DeleteBook;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class DeleteBookCoomandTestes
{
    private readonly FakeData _fakeDate;
    private readonly IBookRepository _bookRepository;

    public DeleteBookCoomandTestes()
    {
        _fakeDate = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Not_Inactivate_Book_As_DeleteBookCommand_Give_Unregistred_Id()
    {
        //Arrange
        var bookDeleteBookCommand = new DeleteBookCommand(10);
        var bookDeleteBookHandler = new DeleteBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookDeleteBookHandler.Handle(bookDeleteBookCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Inactivate_Book_As_DeleteBookCommand_Give_Inactive_Book()
    {
        //Arrange
        var bookDeleteBookCommand = new DeleteBookCommand(4);
        var bookDeleteBookHandler = new DeleteBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookDeleteBookHandler.Handle(bookDeleteBookCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Inactivate_Book_As_DeleteBookCommand_With_LoanStatus_Loaned_Or_Late()
    {
        //Arrange
        var bookDeleteBookCommand = new DeleteBookCommand(2);
        var bookDeleteBookHandler = new DeleteBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookDeleteBookHandler.Handle(bookDeleteBookCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("The book cannot be deleted as it has active loans.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Inactivate_Book_As_DeleteBookCommand_Give_Id()
    {
        //Arrange
        var bookDeleteBookCommand = new DeleteBookCommand(1);
        var bookDeleteBookHandler = new DeleteBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookDeleteBookHandler.Handle(bookDeleteBookCommand, new CancellationToken());
        var bookAfterDelete = await _bookRepository.GetBookByIdAsync(1);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.False(bookAfterDelete.Active);
    }
}
