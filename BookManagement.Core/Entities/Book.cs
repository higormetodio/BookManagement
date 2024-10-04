using BookManagement.Core.Enums;
using BookManagement.Core.Exceptions;
using System;

namespace BookManagement.Core.Entities;
public class Book : BaseEntity
{
    public Book(string title, string author, string iSBN, int publicationYear)
    {
        ValidateCore(title, author, iSBN, publicationYear);
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
        ValidateCore(title, author, iSBN, publicationYear);
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

    public void ValidateCore(string title, string author, string iSBN, int publicationYear)
    {
        CoreExceptionValidation.When(string.IsNullOrEmpty(title), "Invalid Title. Title is required.");
        CoreExceptionValidation.When(string.IsNullOrEmpty(author), "Invalid Author. Author is required.");
        CoreExceptionValidation.When(string.IsNullOrEmpty(iSBN), "Invalid ISBN. ISBN is required.");
        CoreExceptionValidation.When(publicationYear <= 1499 || publicationYear > DateTime.Now.Year, $"Invalid Publication Year. The Publication Year is not in the range between 1499 and {DateTime.Now.Year}");

        Title = title;
        Author = author;
        ISBN = iSBN;
        PublicationYear = publicationYear;
    }
}
