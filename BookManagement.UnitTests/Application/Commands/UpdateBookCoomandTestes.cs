using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class UpdateBookCoomandTestes
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;

    public UpdateBookCoomandTestes()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Not_Update_Book_Properties_Given_UpdateBookCommand_With_Unregistred_Id()
    {
        //Arrange
        var bookUpdateBookCommand = new UpdateBookCommand
        {
            Id = 10,
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        var bookUpdateBookHandler = new UpdateBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookHandler.Handle(bookUpdateBookCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Update_Book_Properties_Given_UpdateBookCommand_With_Inactive_Book()
    {
        //Arrange
        var bookUpdateBookCommand = new UpdateBookCommand
        {
            Id = 4,
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        var bookUpdateBookHandler = new UpdateBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookHandler.Handle(bookUpdateBookCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Book not found", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Update_Book_Properties_Given_UpdateBookCommand()
    {
        //Arrange
        var bookUpdateBookCommand = new UpdateBookCommand
        {
            Id = 1,
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        var bookUpdateBookHandler = new UpdateBookHandler(_bookRepository);

        //Act
        var resultViewModel = await bookUpdateBookHandler.Handle(bookUpdateBookCommand, new CancellationToken());
        var book = await _bookRepository.GetBookByIdAsync(1);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(book);
        Assert.Equal(bookUpdateBookCommand.Id, book.Id);
        Assert.Equal(bookUpdateBookCommand.Title, book.Title);
        Assert.Equal(bookUpdateBookCommand.Author, book.Author);
        Assert.Equal(bookUpdateBookCommand.ISBN, book.ISBN);
        Assert.Equal(bookUpdateBookCommand.PublicationYear, book.PublicationYear);
    }
}
