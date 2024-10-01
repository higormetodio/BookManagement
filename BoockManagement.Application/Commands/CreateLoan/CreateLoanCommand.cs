using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using MediatR;

namespace BookManagement.Application.Commands.CreateLoan;
public class CreateLoanCommand : IRequest<ResultViewModel<int>>
{
    public int UserId { get; set; }
    public int BookId { get; set; }

    public Loan ToEntity()
        => new(UserId, BookId);
}
