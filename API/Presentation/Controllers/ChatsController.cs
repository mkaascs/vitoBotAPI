using Microsoft.AspNetCore.Mvc;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;

using Presentation.Filters;

namespace Presentation.Controllers;

/// <summary>
/// API Controller to interact with chats
/// </summary>
/// <param name="chatService">Required service to interact with chats</param>
[ApiController]
[Route("[controller]")]
[CustomExceptionsHandler]
public class ChatsController(IChatService chatService) : ControllerBase
{
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
    public async Task<IActionResult> RegisterChat(
        RegisterChatCommand command, 
        CancellationToken cancellationToken)
    {
        return await chatService.RegisterNewChatAsync(command, cancellationToken)
            ? CreatedAtAction(nameof(GetChatById), new { chatId = command.Id.ToString() }, command)
            : BadRequest(new ProblemDetails { Title = "Chat with this id already exists"});
    }
    
    /// <summary>
    /// Get all registered chats
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns all registered chats</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ChatViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChats(CancellationToken cancellationToken)
    {
        return Ok(await chatService.GetChatsAsync(cancellationToken));
    }
    
    /// <summary>
    /// Get chat by unique id
    /// </summary>
    /// <param name="chatId">Unique id of chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns found chat</response>
    /// <response code="404">Chat with this id not found</response>
    [HttpGet("{chatId}")]
    [IdValidator]
    [ProducesResponseType(typeof(ChatViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChatById(
        string chatId,
        CancellationToken cancellationToken)
    {
        return Ok(await chatService.GetChatByIdAsync(ulong.Parse(chatId), cancellationToken));
    }
}