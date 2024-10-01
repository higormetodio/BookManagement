﻿namespace BookManagement.Application.Models;
public class UserLoansViewModel
{
    public UserLoansViewModel(int id, string name, string email, IEnumerable<LoanWithUserViewModel> loans)
    {
        Id = id;
        Name = name;
        Email = email;
        Loans = loans;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public IEnumerable<LoanWithUserViewModel> Loans { get; private set; }
}
