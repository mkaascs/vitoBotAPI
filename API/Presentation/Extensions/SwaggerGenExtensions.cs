using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Presentation.Extensions;

internal static class SwaggerGenExtensions 
{
    public static void AddSwaggerDocumentation(this SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "vitoBot API",
            Description = "An API for vitoBot to work, receiving and sending messages using the database",
            Version = "1.0"
        });
        
        options.IncludeXmlComments(Path.Combine(
            AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
    }
}