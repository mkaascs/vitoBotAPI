using FluentValidation;
using FluentValidation.Results;

namespace Presentation.Filters;

public class ModelValidationFilter<TModel> : IEndpointFilter {
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        TModel? model;
        try {
             model = await context.HttpContext.Request.ReadFromJsonAsync<TModel>();
             if (model is null)
                 return Results.BadRequest();
        } 
        
        catch (InvalidOperationException) {
            return Results.BadRequest(new { message = "JSON invalid format" });
        }
        
        IValidator<TModel> modelValidator = 
            context.HttpContext.RequestServices.GetRequiredService<IValidator<TModel>>();

        ValidationResult validationResult = await modelValidator.ValidateAsync(model);

        return validationResult.IsValid
            ? await next(context)
            : TypedResults.BadRequest(validationResult.ToDictionary());
    }
}