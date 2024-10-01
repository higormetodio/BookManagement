using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class BookLoansViewModel
{
    public BookLoansViewModel(int id, string title, int quantity, int loanQuantity, IEnumerable<LoanWithUserViewModel> loans)
    {
        Id = id;
        Title = title;
        Quantity = quantity;
        LoanQuantity = loanQuantity;
        Loans = loans;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public int Quantity { get; private set; }
    public int LoanQuantity { get; private set; }
    public IEnumerable<LoanWithUserViewModel> Loans { get; private set; }
}
