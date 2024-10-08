using BookManagement.Core.Exceptions;

namespace BookManagement.Core.Entities;
public class User : BaseEntity
{
    public User(string name, string email, string password, string role, DateTime birthDate)
    {
        Name = name;
        Email = email;
        Active = true;
        Password = password;
        Role = role;
        BirthDate = birthDate;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool Active { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
    public ICollection<Loan>? Loans { get; private set; }

    public void Update(string name, string email, DateTime birthDate, string password)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Password = password;
    }

    public void ToActive(bool active)
    {
        Active = active;
    }
}
