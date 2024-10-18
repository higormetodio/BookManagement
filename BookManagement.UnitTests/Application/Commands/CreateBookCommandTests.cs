using BookManagement.Application.Commands.CreateBook;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using BookManagement.UnitTests.Infrastructure.FakeAuth;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace BookManagement.UnitTests.Application.Commands;
public class CreateBookCommandTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IAuthService _authService;

    public CreateBookCommandTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Create_Book_As_CreateBookCommand_Returning_Id()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };
        var bookCreateBookHandler = new CreateBookHandler(_bookRepository);


        //Act
        var resultViewModel = await bookCreateBookHandler.Handle(bookCreateBookCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var book = await _bookRepository.GetBookByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.True(id > 0);
        Assert.NotNull(book);
        Assert.Equal(id, book.Id);
        Assert.Equal(bookCreateBookCommand.Title, book.Title);
        Assert.Equal(bookCreateBookCommand.Author, book.Author);
        Assert.Equal(bookCreateBookCommand.ISBN, book.ISBN);
        Assert.Equal(bookCreateBookCommand.PublicationYear, book.PublicationYear);
        Assert.True(book.Active);
        Assert.Equal(0, book.Stock.Quantity);
        Assert.Equal(0, book.Stock.LoanQuantity);
        Assert.False(book.IsReserved);
        Assert.Null(book.Loans);

    }
}
