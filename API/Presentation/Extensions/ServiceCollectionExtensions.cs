using FluentValidation;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;
using Domain.Abstractions;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.Commands.Validation;
using Application.Services;

using Infrastructure;
using Infrastructure.Repositories;

namespace Presentation.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessages(this IServiceCollection services) 
    {
        services.AddScoped<IRepository<Message>, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IValidator<CreateMessageCommand>, CreateMessageCommandValidator>();
        
        return services;
    }

    public static IServiceCollection AddChats(this IServiceCollection services) 
    {
        services.AddScoped<IRepository<Chat>, ChatRepository>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IValidator<RegisterChatCommand>, RegisterChatCommandValidator>();
        
        return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("VitoDbConnectionString");
        services.AddDbContext<ApplicationDbContext>(options
            => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        
        return services;
    }
}