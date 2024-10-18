using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class LoanWithUserViewModel
{
    public LoanWithUserViewModel(int loanId, string userName, string loanDate, string returnDate, string status)
    {
        LoanId = loanId;
        UserName = userName;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int LoanId { get; private set; }
    public string UserName { get; private set; }
    public string LoanDate { get; private set; }
    public string ReturnDate { get; private set; }
    public string Status { get; private set; }

    public static LoanWithUserViewModel FromEntity(Loan entity)
        => new(entity.Id, entity.User.Name, entity.LoanDate.ToString("MM-dd-yyyy"), entity.ReturnDate.ToString("MM-dd-yyyy"), entity.Status.ToString());
}
