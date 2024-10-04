using BookManagement.Application.Commands.UpdateBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookManagement.API.Filters;

public class ApiValidationFilter : IActionFilter
{
    public async void OnActionExecuted(ActionExecutedContext context)
    {
        

    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {        
        if (!context.ModelState.IsValid)
        {
            var messages = context.ModelState
                                  .SelectMany(m => m.Value.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();

            context.Result = new BadRequestObjectResult(messages);
        }
    }
}
