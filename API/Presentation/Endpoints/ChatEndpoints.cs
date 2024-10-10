using Carter;

using Application.Abstractions;
using Application.DTO.Commands;
using Presentation.Filters;

namespace Presentation.Endpoints;

public class ChatEndpoints : ICarterModule {
    public void AddRoutes(IEndpointRouteBuilder builder) {
        RouteGroupBuilder chatApi = builder.MapGroup("chats/");
        
        chatApi.MapGet("/", async (
            IChatService chatService,
            CancellationToken cancellationToken) 
                => TypedResults.Ok(await chatService.GetChatsAsync(cancellationToken)));

        chatApi.MapPost("/", async (
            IChatService chatService,
            RegisterChatCommand command,
            CancellationToken cancellationToken) 
                => await chatService.RegisterNewChatAsync(command, cancellationToken) 
                    ? TypedResults.Created() 
                    : Results.BadRequest(new { message = "A chat with the specified id already exists" }))
            .AddEndpointFilter<ModelValidationFilter<RegisterChatCommand>>();
    }
}