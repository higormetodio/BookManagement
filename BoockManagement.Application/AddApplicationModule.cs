using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Application;
public static class AddApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
                            config.RegisterServicesFromAssemblyContaining<CreateBookCommand>());
        
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateBookValidator>();

        return services;
    }
}
