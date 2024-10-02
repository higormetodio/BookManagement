using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using MediatR;

namespace BookManagement.Application.Commands.CreateUser;
public class CreateUserCommand : IRequest<ResultViewModel<int>>
{
    public string Name { get; set; }
    public string Email { get; set; }

    public User ToEntity()
        => new(Name, Email);
}
