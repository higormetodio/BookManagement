using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class BookDetailViewModelTests
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;

    public BookDetailViewModelTests()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }

    [Fact]
    public async Task Should_Create_Book_Detail_View_Model_With_Correct_Properties()
    {
        //Arrange
        var book = await _bookRepository.GetBookByIdAsync(1);

        //Act
        var bookDetailViewModel = BookDetailViewModel.FromEntity(book);

        //Assert
        Assert.NotNull(bookDetailViewModel);
        Assert.Equal(book.Id, bookDetailViewModel.Id);
        Assert.Equal(book.Title, bookDetailViewModel.Title);
        Assert.Equal(book.Author, bookDetailViewModel.Author);
        Assert.Equal(book.ISBN, bookDetailViewModel.ISBN);
        Assert.Equal(book.PublicationYear, bookDetailViewModel.PublicationYear);
        Assert.Equal(book.Stock.Quantity, bookDetailViewModel.Quantity);
        Assert.Equal(book.Stock.LoanQuantity, bookDetailViewModel.LoanQuantity);
    }
}
