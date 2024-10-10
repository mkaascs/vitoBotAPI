using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

public class IdValidationFilterAttribute : Attribute, IActionFilter {
    public string RouteName { get; set; } = "id";
    
    public void OnActionExecuting(ActionExecutingContext context) {
        if (context.HttpContext.GetRouteValue(RouteName) is string id)
            if (!ulong.TryParse(id, out _))
                context.Result = new BadRequestObjectResult("Id is incorrect and can't be converted to ulong");
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}