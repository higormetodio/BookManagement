using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class BookviewModel
{
    public BookviewModel(int id, string title, string author, int publicationYear, bool isReserved)
    {
        Id = id;
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
        IsReserved = isReserved;        
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int PublicationYear { get; set; }
    public bool IsReserved { get; private set; }

    public static BookviewModel FromEntity(Book entity)
        => new(entity.Id, entity.Title, entity.Author, entity.PublicationYear, entity.IsReserved);
}
