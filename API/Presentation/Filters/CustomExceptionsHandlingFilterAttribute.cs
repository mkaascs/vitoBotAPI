using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Application.Exceptions;

using Domain.Exceptions;

namespace Presentation.Filters;

public class CustomExceptionsHandlingFilterAttribute : Attribute, IExceptionFilter {
    public void OnException(ExceptionContext context) {
        context.Result = context.Exception switch {
            ChatWasNotFoundException => new NotFoundObjectResult("Chat was not found"),
            ValidationProblemException validationProblems => new BadRequestObjectResult(validationProblems.Errors),
            _ => context.Result
        };
    }
}