﻿using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Application.Commands.DeleteUser;
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByIdAsync(request.Id);

        if (user is null)
        {
            return ResultViewModel.Error("User not found.");
        }

        user.ToActive(false);

        await _repository.UpdateUserAsync(user);

        return ResultViewModel.Success();
    }
}