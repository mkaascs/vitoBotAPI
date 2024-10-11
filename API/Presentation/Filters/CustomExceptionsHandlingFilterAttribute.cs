using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Application.Exceptions;

using Domain.Exceptions;

namespace Presentation.Filters;

/// <summary>
/// Exception filter to catch custom exceptions and return status codes
/// </summary>
public class CustomExceptionsHandlingFilterAttribute : Attribute, IExceptionFilter {
    /// <summary>
    /// Catch custom exceptions and return status codes
    /// </summary>
    /// <param name="context">An exception context</param>
    public void OnException(ExceptionContext context) {
        context.Result = context.Exception switch {
            ChatWasNotFoundException exception => 
                new NotFoundObjectResult(new ProblemDetails { Title = "Chat was not found"}),
            
            ValidationProblemException validationProblems => 
                new BadRequestObjectResult(new HttpValidationProblemDetails(validationProblems.Errors)),
            
            _ => context.Result
        };
    }
}