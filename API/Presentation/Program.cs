using Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.Services.AddChats();
builder.Services.AddMessages();

builder.Services.AddApplicationDbContext(builder.Configuration);

#endregion

WebApplication application = builder.Build();

application.UseStatusCodePages();
application.MapControllers();

application.Run();