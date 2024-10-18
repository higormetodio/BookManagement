using BookManagement.Core.Entities;

namespace BookManagement.UnitTests.Core.Entities;
public class BookStockTests
{
    private readonly Book _book = new("Algoritmos - Teoria e Prática", "Thomas H. Cormen", "978-8535236996", 2012);
    private const int _quantity = 10;

    [Fact]
    public void Should_Create_Stock_Properties_After_Create_New_Book()
    {
        //Arrange


        //Act
        var stock = _book.Stock;

        //Assert
        Assert.Equal(_book.Id, stock.BookId);
        Assert.Equal(0, stock.Quantity);
        Assert.Equal(0, stock.LoanQuantity);
        Assert.Null(stock.Book);
    }

    [Fact]
    public void Should_Update_Quantity_Book_Stock()
    {
        //Arrange

        //Act
        var stock = _book.Stock;
        stock.Update(_quantity);

        //Assert
        Assert.NotEqual(0, stock.Quantity);
    }

    [Fact]
    public void Should_Update_Quantity_LoanQuantity_Book_Stock_After_LoanStockMovement()
    {
        //Arrange


        //Act
        var stock = _book.Stock;
        stock.Update(_quantity);
        stock.LoanStockMovement();

        //Assert
        Assert.Equal(9, stock.Quantity);
        Assert.Equal(1, stock.LoanQuantity);
    }

    [Fact]
    public void Should_Update_Quantity_LoanQuantity_Book_Stock_After_ReturnStockMovement()
    {
        //Arrange


        //Act
        var stock = _book.Stock;
        stock.Update(_quantity);
        stock.LoanStockMovement();
        stock.ReturnStockMovement();

        //Assert
        Assert.Equal(10, stock.Quantity);
        Assert.Equal(0, stock.LoanQuantity);
    }
}
