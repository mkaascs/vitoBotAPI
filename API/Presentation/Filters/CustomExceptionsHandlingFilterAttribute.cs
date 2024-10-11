using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Application.Exceptions;

using Domain.Exceptions;

namespace Presentation.Filters;

public class CustomExceptionsHandlingFilterAttribute : Attribute, IExceptionFilter {
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