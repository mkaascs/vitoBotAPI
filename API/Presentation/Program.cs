using Carter;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddCarter();
builder.Services.AddProblemDetails();

#endregion

WebApplication application = builder.Build();

application.UseStatusCodePages();
application.UseExceptionHandler();

application.MapCarter();

application.Run();