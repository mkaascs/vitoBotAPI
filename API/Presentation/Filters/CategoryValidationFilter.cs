using Domain.Entities.Enums;

namespace Presentation.Filters;

public class CategoryValidationFilter : IEndpointFilter {
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        if (context.HttpContext.GetRouteValue("category") is string category)
            if (!Enum.TryParse(typeof(ContentType), category, out _))
                return Results.BadRequest(new { message = "Category is not correct" });

        return await next(context);
    }
}