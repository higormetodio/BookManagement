﻿using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Queries.GetUserLoans;
public class GetUserLoanHandler : IRequestHandler<GetUserLoansQuery, ResultViewModel<UserLoansViewModel>>
{
    private readonly IUserRepository _repository;

    public GetUserLoanHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<UserLoansViewModel>> Handle(GetUserLoansQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserLoansByIdAsync(request.Id);

        if (user is null || !user.Active)
        {
            return ResultViewModel<UserLoansViewModel>.Error("User not found.");
        }

        var loans = user.Loans.Select(LoanWithUserViewModel.FromEntity);

        var model = new UserLoansViewModel(user.Id, user.Name, user.Email, loans);

        return ResultViewModel<UserLoansViewModel>.Success(model);
    }
}