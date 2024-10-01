using BookManagement.Core.Exceptions;

namespace BookManagement.Core.Entities;
public class BookStock : BaseEntity
{
    public BookStock(int quantity, int bookId)
    {
        Quantity = quantity;
        LoanQuantity = 0;
        BookId = bookId;
    }

    public int Quantity { get; private set; }
    public int LoanQuantity { get; private set; }
    public int BookId { get; private set; }
    public Book? Book { get; private set; }


    public void Update(int quantity)
    {
        CoreExceptionValidation.When(quantity < 0, "Invalid Quantity. The Quantity must be greater than or equal to zero.");

        Quantity = quantity;
    }

    public void LoanStockMovement()
    {
        Quantity -= 1;
        LoanQuantity += 1;
    }

    public void ReturnStockMovement()
    {
        Quantity += 1;
        LoanQuantity -= 1;
    }
}
