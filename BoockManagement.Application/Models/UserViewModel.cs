using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class UserViewModel
{
    public UserViewModel(int id, string name, string email, DateTime birthDate, string password, bool active)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        Active = active;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Password { get; private set; }
    public bool Active { get; private set; }


    public static UserViewModel FromEntity(User user)
        => new(user.Id, user.Name, user.Email, user.BirthDate, user.Password, user.Active);
}
