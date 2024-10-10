using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.Extensions;

internal static class ExceptionMiddlewareExtensions {
    public static void UseMinimalApiExceptionHandler(this IApplicationBuilder application) {
        application.UseExceptionHandler(errorApp => 
            errorApp.Run(async context => {
                IExceptionHandlerFeature? errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature is null)
                    return;
                
                Exception exception = errorFeature.Error;
                await context.HandleExceptionAsync(exception);
            }));
    }
}