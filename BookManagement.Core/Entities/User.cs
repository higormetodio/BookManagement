using BookManagement.Core.Exceptions;

namespace BookManagement.Core.Entities;
public class User : BaseEntity
{
    public User(string name, string email)
    {
        ValidateCore(name, email);
        Active = true;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool Active { get; private set; }
    public ICollection<Loan>? Loans { get; private set; }

    public void Update(string email)
    {
        CoreExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid Email. Email is required");
        Email = email;
    }

    public void ValidateCore(string name, string email)
    {
        CoreExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name. Name is required");
        CoreExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid Email. Email is required");

        Name = name;
        Email = email;
    }
}
