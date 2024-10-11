using Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.Services.AddChats();
builder.Services.AddMessages();

builder.Services.AddSwaggerGen(options 
    => options.AddSwaggerDocumentation());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationDbContext(builder.Configuration);

#endregion

WebApplication application = builder.Build();

if (application.Environment.IsDevelopment()) {
    application.UseSwagger();
    application.UseSwaggerUI();
}

application.UseStatusCodePages();
application.MapControllers();

application.Run();