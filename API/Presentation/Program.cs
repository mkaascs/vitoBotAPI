WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

#endregion

WebApplication application = builder.Build();

application.UseStatusCodePages();
application.MapControllers();

application.Run();