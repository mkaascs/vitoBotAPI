using System.Text.Json;
using System.Text.Json.Serialization;

using Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddChats();
builder.Services.AddMessages();

builder.Services.AddSwaggerGen(options 
    => options.AddSwaggerDocumentation());

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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