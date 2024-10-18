using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class UpdateBookOnlyActiveCoomandTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;

    public UpdateBookOnlyActiveCoomandTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Not_Update_Book_Status_Property_Given_UpdateBookOnlyActiveCommand_With_Unregistred_Id()
    {
        //Arrange
        var bookUpdateBookOnlyActiveCommand = new UpdateBookOnlyActiveCommand(10)
        {
            Active = true
        };

        var bookUpdateBookOnlyActiveHandler = new UpdateBookOnlyActiveHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookOnlyActiveHandler.Handle(bookUpdateBookOnlyActiveCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Update_Book_Properties_Given_UpdateBookCommand_With_Book_Already_Active()
    {
        //Arrange
        var bookUpdateBookOnlyActiveCommand = new UpdateBookOnlyActiveCommand(10)
        {
            Active = true
        };

        var bookUpdateBookOnlyActiveHandler = new UpdateBookOnlyActiveHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookOnlyActiveHandler.Handle(bookUpdateBookOnlyActiveCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book is already active.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Update_Book_Status_Property_Given_UpdateBookOnlyActiveCommand()
    {
        //Arrange
        var bookUpdateBookOnlyActiveCommand = new UpdateBookOnlyActiveCommand(4)
        {
            Active = true
        };

        var bookUpdateBookOnlyActiveHandler = new UpdateBookOnlyActiveHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookOnlyActiveHandler.Handle(bookUpdateBookOnlyActiveCommand, new CancellationToken());
        var book = await _bookRepository.GetBookByIdAsync(4);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(book);
        Assert.Equal(bookUpdateBookOnlyActiveCommand.Id, book.Id);
        Assert.Equal(bookUpdateBookOnlyActiveCommand.Active, book.Active);
        Assert.True(book.Active);
    }
}
