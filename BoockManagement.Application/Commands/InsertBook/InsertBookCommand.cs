using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using MediatR;

namespace BookManagement.Application.Commands.InsertBook;
public class InsertBookCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }

    public Book ToEntity()
        => new(Title, Author, ISBN, PublicationYear);
}
