using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class LoanViewModel
{
    public LoanViewModel(int id, string userName, string bookTitle, DateTime loanDate, DateTime returnDate, string status)
    {
        Id = id;
        UserName = userName;
        BookTitle = bookTitle;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int Id { get; private set; }
    public string UserName { get; private set; }
    public string BookTitle { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public string Status { get; private set; }
    public static LoanViewModel FromEntity(Loan loan)
        => new(loan.Id, loan.User.Name, loan.Book.Title, loan.LoanDate, loan.ReturnDate, loan.Status.ToString());
}
