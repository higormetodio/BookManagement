using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class BookViewModelTests
{
    private readonly IBookRepository _bookRepository;
    public BookViewModelTests()
    {
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Create_Book_View_Model_With_Correct_Properties()
    {
        //Arrange
        var book = await _bookRepository.GetBookByIdAsync(1);

        //Act
        var bookViewModel = BookViewModel.FromEntity(book);

        //Assert
        Assert.NotNull(bookViewModel);
        Assert.Equal(book.Id, bookViewModel.Id);
        Assert.Equal(book.Title, bookViewModel.Title);
        Assert.Equal(book.Author, bookViewModel.Author);
        Assert.Equal(book.PublicationYear, bookViewModel.PublicationYear);
        Assert.Equal(book.IsReserved, bookViewModel.IsReserved);
    }
}
