using BookManagement.Application.BackgroundServices;
using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Validators;
using BookManagement.Application.Email;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence.Repositories;

namespace BookManagement.Application;
public static class AddApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
                            config.RegisterServicesFromAssemblyContaining<CreateBookCommand>());
        
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateBookValidator>();

        services.AddSingleton<IEmailService, EmailService>();
        services.AddHostedService<BeforeLateLoanNotificationService>();
        services.AddHostedService<LateLoanNotificationService>();

        return services;
    }
}
