using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBook;
public class UpdateBookCommand : IRequest<ResultViewModel>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
}
