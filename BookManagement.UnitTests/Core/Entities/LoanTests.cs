using BookManagement.Core.Entities;
using BookManagement.Core.Enums;

namespace BookManagement.UnitTests.Core.Entities;
public class LoanTests
{
    private readonly Book _book = new("Algoritmos - Teoria e Prática", "Thomas H. Cormen", "978-8535236996", 2012);
    private readonly User _user = new("Antônio Albuquerque", "antonio@gmail.com", "123456789", "user", new DateTime(1979, 12, 4));

    [Fact]
    public void Should_Create_New_Loan_With_Corret_Properties()
    {
        //Arrange


        //Act
        var loan = new Loan(_user.Id, _book.Id);

        var dateNotWeekEnd = loan.LoanDate.AddDays(15);

        while (dateNotWeekEnd.DayOfWeek == DayOfWeek.Saturday || dateNotWeekEnd.DayOfWeek == DayOfWeek.Sunday)
        {
            dateNotWeekEnd = dateNotWeekEnd.AddDays(1);
        }

        //Assert
        Assert.Equal(_user.Id, loan.UserId);
        Assert.Equal(_book.Id, loan.BookId);
        Assert.Equal(DateTime.Now.ToString("MM/dd/yyyy"), loan.LoanDate.ToString("MM/dd/yyyy"));
        Assert.Equal(dateNotWeekEnd.ToString("MM/dd/yyyy"), loan.ReturnDate.ToString("MM/dd/yyyy"));
        Assert.Equal(LoanStatus.Loaned, loan.Status);
        Assert.Null(loan.Book);
        Assert.Null(loan.User);
    }

    [Fact]
    public void Shold_Update_Loan_When_Returned()
    {
        //Arrange
        var loan = new Loan(_user.Id, _book.Id);

        //Act
        loan.LoanReturned();

        //Assert
        Assert.Equal(DateTime.Now.ToString("MM/dd/yyyy"), loan.ReturnDate.ToString("MM/dd/yyyy"));
        Assert.Equal(LoanStatus.Returned, loan.Status);
    }

    [Fact]
    public void Shold_Update_Loan_When_Late()
    {
        //Arrange
        var loan = new Loan(_user.Id, _book.Id);

        //Act
        loan.LoanLate();

        //Assert
        Assert.Equal(LoanStatus.Late, loan.Status);
    }
}
