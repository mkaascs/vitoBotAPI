using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Domain.Entities.Enums;

namespace Presentation.Filters;

public class CategoryValidationFilterAttribute : Attribute, IActionFilter {
    public string RouteName { get; set; } = "category";
    
    public void OnActionExecuting(ActionExecutingContext context) {
        if (context.HttpContext.GetRouteValue(RouteName) is string category)
            if (!Enum.TryParse<ContentType>(category, out _))
                context.Result = new BadRequestObjectResult("Category is incorrect");
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}