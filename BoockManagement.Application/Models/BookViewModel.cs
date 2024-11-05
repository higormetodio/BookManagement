using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class BookViewModel
{
    public BookViewModel(int id, string title, string author, int publicationYear, bool active)
    {
        Id = id;
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
        Active = active;        
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int PublicationYear { get; set; }
    public bool Active { get; private set; }

    public static BookViewModel FromEntity(Book entity)
        => new(entity.Id, entity.Title, entity.Author, entity.PublicationYear, entity.Active);
}
