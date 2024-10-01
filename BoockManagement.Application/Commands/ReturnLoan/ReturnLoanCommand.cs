using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.ReturnLoan;
public class ReturnLoanCommand : IRequest<ResultViewModel>
{
    public ReturnLoanCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
