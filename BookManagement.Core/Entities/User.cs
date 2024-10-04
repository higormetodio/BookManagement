using BookManagement.Core.Exceptions;

namespace BookManagement.Core.Entities;
public class User : BaseEntity
{
    public User(string name, string email)
    {
        Name = name;
        Email = email;
        Active = true;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool Active { get; private set; }
    public ICollection<Loan>? Loans { get; private set; }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void ToActive(bool active)
    {
        Active = active;
    }
}
