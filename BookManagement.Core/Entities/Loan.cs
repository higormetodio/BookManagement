using BookManagement.Core.Enums;

namespace BookManagement.Core.Entities;
public class Loan : BaseEntity
{
    public Loan(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        LoanDate = DateTime.Now;
        ReturnDate = ReturnDateCalculate(LoanDate);
        Status = LoanStatus.Loaned;
    }

    public int UserId { get; private set; }
    public int BookId { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public LoanStatus Status { get; private set; }
    public User? User { get; private set; }
    public Book? Book { get; private set; }

    public void LoanReturned()
    {
        ReturnDate = DateTime.Now;
        Status = LoanStatus.Returned;
    }

    public void LoanLate()
    {
        Status = LoanStatus.Late;
    }

    private DateTime ReturnDateCalculate(DateTime loanDate)
    {
        var returnDate = loanDate.AddDays(15);

        while (returnDate.DayOfWeek == DayOfWeek.Saturday || returnDate.DayOfWeek == DayOfWeek.Sunday)
        {
            returnDate = returnDate.AddDays(1);
        }

        return returnDate;
    }
}
