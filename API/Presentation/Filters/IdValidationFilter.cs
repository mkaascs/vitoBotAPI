namespace Presentation.Filters;

public class IdValidationFilter : IEndpointFilter {
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        if (context.HttpContext.GetRouteValue("id") is string id)
            if (!ulong.TryParse(id, out _))
                return Results.BadRequest(new { message = "Id is not correct" });

        return await next(context);
    }
}