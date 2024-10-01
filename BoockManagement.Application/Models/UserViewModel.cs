using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class UserViewModel
{
    public UserViewModel(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public static UserViewModel FromEntity(User user)
        => new(user.Id, user.Name, user.Email);
}
