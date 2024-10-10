using Carter;
using Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddCarter();
builder.Services.AddProblemDetails();

#endregion

WebApplication application = builder.Build();

application.UseStatusCodePages();
application.UseMinimalApiExceptionHandler();

application.MapCarter();

application.Run();