using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class LoanWithBookViewModel
{
    public LoanWithBookViewModel(int loanId, string bookTitle, DateTime loanDate, DateTime returnDate, string status)
    {
        LoanId = loanId;
        BookTitle = bookTitle;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int LoanId { get; private set; }
    public string BookTitle { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public string Status { get; private set; }

    public static LoanWithBookViewModel FromEntity(Loan entity)
        => new(entity.Id, entity.Book.Title, entity.LoanDate, entity.ReturnDate, entity.Status.ToString());
}
