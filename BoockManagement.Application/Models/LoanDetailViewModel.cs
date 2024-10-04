﻿using BookManagement.Core.Entities;
using BookManagement.Core.Enums;

namespace BookManagement.Application.Models;
public class LoanDetailViewModel
{
    public LoanDetailViewModel(int id, int userId, int bookId, string userName, string bookTitle, string loanDate, string returnDate, string status)
    {
        Id = id;
        UserId = userId;
        BookId = bookId;
        UserName = userName;
        BookTitle = bookTitle;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string UserName { get; private set; }
    public int BookId { get; private set; }    
    public string BookTitle { get; private set; }
    public string LoanDate { get; private set; }
    public string ReturnDate { get; private set; }
    public string Status { get; private set; }

    public static LoanDetailViewModel FromEntity(Loan loan)
        => new(loan.Id, loan.UserId, loan.BookId, loan.User.Name, loan.Book.Title, loan.LoanDate.ToString("MM-dd-yyyy"), loan.ReturnDate.ToString("MM-dd-yyyy"), loan.Status.ToString());
}