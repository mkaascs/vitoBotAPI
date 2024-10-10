using Domain.Exceptions;

using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions;

public static class ExceptionExtensions {
    public static async Task HandleExceptionAsync(this HttpContext context, Exception exception) {
        if (exception is ChatWasNotFoundException)
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        
        else context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        ProblemDetails response = new ProblemDetails {
            Title = "Error",
            Detail = exception.Message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}