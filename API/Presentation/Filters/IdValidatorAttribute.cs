using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

/// <summary>
/// Filter to check id value is valid
/// </summary>
public class IdValidatorAttribute : Attribute, IActionFilter
{
    /// <summary>
    /// A route name of id value
    /// </summary>
    public string RouteName { get; set; } = "id";
    
    /// <summary>
    /// Checking id value
    /// </summary>
    /// <param name="context">An action context</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.GetRouteValue(RouteName) is string id)
            if (!ulong.TryParse(id, out _))
                context.Result = new BadRequestObjectResult(new ProblemDetails { Title = "Id is incorrect and can't be converted to ulong"});
    }

    /// <summary>
    /// After call action
    /// </summary>
    /// <param name="context">An action context</param>
    public void OnActionExecuted(ActionExecutedContext context) { }
}