using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace BookManagement.Application.Commands.CreateUser;
public class CreateUserCommand : IRequest<ResultViewModel<int>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public string Role { get; set; } = "user";

    public User ToEntity(string password)
        => new(Name, Email, password, Role, BirthDate);
}
