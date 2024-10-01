using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class BookDetailViewModel
{
    public BookDetailViewModel(int id, string title, string author, string iSBN, int publicationYear, int quantity, int loanQuantity)
    {
        Id = id;
        Title = title;
        Author = author;
        ISBN = iSBN;
        PublicationYear = publicationYear;
        Quantity = quantity;
        LoanQuantity = loanQuantity;
    }

    public int Id { get; set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public int PublicationYear { get; private set; }
    public int Quantity { get; private set; }
    public int LoanQuantity { get; private set; }

    public static BookDetailViewModel FromEntity(Book entity)
        => new(entity.Id, entity.Title, entity.Author, entity.ISBN, entity.PublicationYear, entity.Stock.Quantity, entity.Stock.LoanQuantity);
}
