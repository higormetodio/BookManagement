﻿using BookManagement.Application.Commands.InsertBook;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Application;
public static class AddApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
                            config.RegisterServicesFromAssemblyContaining<InsertBookCommand>());

        return services;
    }
}
