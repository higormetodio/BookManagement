﻿using BookManagement.Core.Entities;

namespace BookManagement.Application.Models;
public class LoanWithBookViewModel
{
    public LoanWithBookViewModel(int loanId, string bookTitle, string loanDate, string returnDate, string status)
    {
        LoanId = loanId;
        BookTitle = bookTitle;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int LoanId { get; private set; }
    public string BookTitle { get; private set; }
    public string LoanDate { get; private set; }
    public string ReturnDate { get; private set; }
    public string Status { get; private set; }

    public static LoanWithBookViewModel FromEntity(Loan entity)
        => new(entity.Id, entity.Book.Title, entity.LoanDate.ToString("MM-dd-yyyy"), entity.ReturnDate.ToString("MM-dd-yyyy"), entity.Status.ToString());
}
