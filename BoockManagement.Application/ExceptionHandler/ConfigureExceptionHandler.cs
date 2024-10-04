using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Application.ExceptionHandler;
public class ConfigureExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var feature = httpContext.Features.Get<IExceptionHandlerFeature>();

        var errorDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error",
            Detail = feature.Error.Message
        };

        await httpContext.Response.WriteAsJsonAsync(errorDetails, cancellationToken);

        return true;
    }
}
