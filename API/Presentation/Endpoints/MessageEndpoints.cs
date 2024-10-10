using Carter;

using Domain.Entities.Enums;

using Application.Abstractions;
using Application.DTO.Commands;

using Presentation.Filters;

namespace Presentation.Endpoints;

public class MessageEndpoints : ICarterModule {
    public void AddRoutes(IEndpointRouteBuilder builder) {
        RouteGroupBuilder messageApi = builder.MapGroup("/{id}")
            .AddEndpointFilter<IdValidationFilter>();

        messageApi.MapGet("/", async (
            string id,
            IMessageService messageService,
            CancellationToken cancellationToken)
                => TypedResults.Ok(await messageService.GetMessagesFromChatAsync(
                    ulong.Parse(id), cancellationToken)));

        messageApi.MapGet("/random", async (
            string id,
            IMessageService messageService,
            CancellationToken cancellationToken)
                => TypedResults.Ok(await messageService.GetRandomMessageFromChatAsync(
                    ulong.Parse(id), cancellationToken)));

        messageApi.MapPost("/", async (
            string id,
            CreateMessageCommand command,
            IMessageService messageService,
            CancellationToken cancellationToken) 
                => TypedResults.Ok(await messageService.AddNewMessageAsync(
                    ulong.Parse(id), command, cancellationToken)))
            .AddEndpointFilter<ModelValidationFilter<CreateMessageCommand>>();

        
        RouteGroupBuilder messageCategoryApi = messageApi.MapGroup("/{category}")
            .AddEndpointFilter<CategoryValidationFilter>();

        messageCategoryApi.MapGet("/", async (
            string id,
            string category,
            IMessageService messageService,
            CancellationToken cancellationToken)
                => TypedResults.Ok(await messageService.GetMessagesFromChatAsync(
                    ulong.Parse(id), Enum.Parse<ContentType>(category), cancellationToken)));

        messageCategoryApi.MapGet("/random", async (
            string id,
            string category,
            IMessageService messageService,
            CancellationToken cancellationToken) 
                => TypedResults.Ok(await messageService.GetRandomMessageFromChatAsync(
                    ulong.Parse(id), Enum.Parse<ContentType>(category), cancellationToken)));
    }
}