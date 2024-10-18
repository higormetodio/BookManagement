using BookManagement.Core.Entities;

namespace BookManagement.UnitTests.Core.Entities;
public class BookTests
{
    [Fact]
    public void Should_Create_New_Book_With_Corret_Properties()
    {
        //Arrange
        var title = "Algoritmos - Teoria e Prática";
        var author = "Thomas H. Cormen";
        var isbn = "978-8535236996";
        var publicationYear = 2012;

        //Act
        Book book = new (title, author, isbn, publicationYear);

        //Assert
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.Equal(isbn, book.ISBN);
        Assert.Equal(publicationYear, book.PublicationYear);
        Assert.False(book.IsReserved);
        Assert.True(book.Active);
        Assert.NotNull(book.Stock);
        Assert.Null(book.Loans);
    }
    
    [Fact]
    public void Should_Create_New_Book_With_Zero_Stock()
    {
        //Arrange
        var title = "Algoritmos - Teoria e Prática";
        var author = "Thomas H. Cormen";
        var isbn = "978-8535236996";
        var publicationYear = 2012;

        //Act
        Book book = new(title, author, isbn, publicationYear);

        //Assert
        Assert.Equal(0, book.Stock.Quantity);
    }

    [Fact]
    public void Should_Update_Book_With_New_Data()
    {
        //Arrange
        var title = "Algoritmos - Teoria e Prática";
        var author = "Thomas H. Cormen";
        var isbn = "978-8535236996";
        var publicationYear = 2012;

        var newTitle = "Código Limpo: Habilidades Práticas do Agile Software";
        var newAuthor = "Robert C. Martin";
        var newISBN = "978-8576082675";
        var newPublicationYear = 2009;

        Book book = new(title, author, isbn, publicationYear);

        //Act
        book.Update(newTitle, newAuthor, newISBN, newPublicationYear);

        //Assert
        Assert.NotEqual(title, book.Title);
        Assert.NotEqual(author, book.Author);
        Assert.NotEqual(isbn, book.ISBN);
        Assert.NotEqual(publicationYear, book.PublicationYear);
    }

    [Fact]
    public void Should_Update_Book_Only_Activete_Propertie()
    {
        //Arrange
        var title = "Algoritmos - Teoria e Prática";
        var author = "Thomas H. Cormen";
        var isbn = "978-8535236996";
        var publicationYear = 2012;

        Book book = new(title, author, isbn, publicationYear);

        var active = false;

        //Act
        book.ToActive(active);

        //Assert
        Assert.False(book.Active);
    }
}
