using BookManagement.Core.Exceptions;

namespace BookManagement.Core.Entities;
public class Book : BaseEntity
{
    public Book(string title, string author, string iSBN, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = iSBN;
        PublicationYear = publicationYear;
        IsReserved = false;
        Active = true;

        Stock = new(0, Id);
        
    }

    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public int PublicationYear { get; private set; }
    public bool IsReserved { get; private set; }
    public bool Active { get; private set; }
    public BookStock Stock { get; private set; }
    public ICollection<Loan> Loans { get; private set; }


    public void Update(string title, string author, string iSBN, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = iSBN;
        PublicationYear = publicationYear;
    }
    
    public void ToReserve(bool isReserved)
    {
        CoreExceptionValidation.When(Stock.Quantity > 0, $"There is still {Stock.Quantity} this book");

        IsReserved = isReserved; 
    }

    public void ToActive(bool active)
    {
        Active = active;
    }
}
