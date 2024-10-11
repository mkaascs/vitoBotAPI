using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Domain.Entities.Enums;

namespace Presentation.Filters;

/// <summary>
/// Filter to check category value is valid
/// </summary>
public class CategoryValidationFilterAttribute : Attribute, IActionFilter {
    /// <summary>
    /// A route name of category value
    /// </summary>
    public string RouteName { get; set; } = "category";
    
    /// <summary>
    /// Checking category value
    /// </summary>
    /// <param name="context">An action context</param>
    public void OnActionExecuting(ActionExecutingContext context) {
        if (context.HttpContext.GetRouteValue(RouteName) is string category)
            if (!Enum.TryParse<ContentType>(category, true, out _))
                context.Result = new BadRequestObjectResult(new ProblemDetails { Title = "Category is incorrect"});
    }

    /// <summary>
    /// After call action
    /// </summary>
    /// <param name="context">An action context</param>
    public void OnActionExecuted(ActionExecutedContext context) { }
}