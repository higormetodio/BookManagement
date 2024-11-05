using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class LoanWithUserViewModel
{
    public LoanWithUserViewModel(int loanId, string userName, DateTime loanDate, DateTime returnDate, string status)
    {
        LoanId = loanId;
        UserName = userName;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int LoanId { get; private set; }
    public string UserName { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public string Status { get; private set; }

    public static LoanWithUserViewModel FromEntity(Loan entity)
        => new(entity.Id, entity.User.Name, entity.LoanDate, entity.ReturnDate, entity.Status.ToString());
}
