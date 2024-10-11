using Microsoft.AspNetCore.Mvc;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;
using Presentation.Filters;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[CustomExceptionsHandlingFilter]
public class ChatsController(IChatService chatService) : ControllerBase {
    /// <summary>
    /// Get all registered chats
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns all registered chats</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MessageViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChats(CancellationToken cancellationToken) 
        => Ok(await chatService.GetChatsAsync(cancellationToken));

    
    /// <summary>
    /// Register a new chat
    /// </summary>
    /// <param name="command">Instance of <see cref="RegisterChatCommand"/> to register new chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="201">Chat was registered successfully</response>
    /// <response code="400">Command model is incorrect. Also return if chat with this id already exists</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterChat(RegisterChatCommand command, CancellationToken cancellationToken) {
        return await chatService.RegisterNewChatAsync(command, cancellationToken)
            ? Created()
            : BadRequest();
    }
}