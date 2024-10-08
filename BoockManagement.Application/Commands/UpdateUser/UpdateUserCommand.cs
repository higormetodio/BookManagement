using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.UpdateUser;
public class UpdateUserCommand : IRequest<ResultViewModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
}
