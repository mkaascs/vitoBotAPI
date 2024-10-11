using FluentValidation;
using Microsoft.EntityFrameworkCore;

using Domain.Abstractions;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.Commands.Validation;
using Application.Services;

using Infrastructure;
using Infrastructure.Repositories;

namespace Presentation.Extensions;

internal static class ServiceCollectionExtensions {
    public static IServiceCollection AddMessages(this IServiceCollection services) {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IValidator<CreateMessageCommand>, CreateMessageCommandValidator>();
        return services;
    }

    public static IServiceCollection AddChats(this IServiceCollection services) {
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<ChatRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IValidator<RegisterChatCommand>, RegisterChatCommandValidator>();
        return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
        IConfiguration configuration) {
        
        string? connectionString = configuration.GetConnectionString("VitoBotDb");
        services.AddDbContext<ApplicationDbContext>(options
            => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        return services;
    }
}