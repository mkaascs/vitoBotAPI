using Microsoft.AspNetCore.Mvc;

using Domain.Entities.Enums;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;

using Presentation.Filters;

namespace Presentation.Controllers;

[ApiController]
[Route("{id}")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[IdValidationFilter, CustomExceptionsHandlingFilter]
public class MessageController(IMessageService messageService) : ControllerBase {
    /// <summary>
    /// Adds new message in a specific chat
    /// </summary>
    /// <param name="id">ID of a specific chat</param>
    /// <param name="command">Instance of <see cref="CreateMessageCommand"/> to create new message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="201">Message was created successfully</response>
    /// <response code="400">Chat id or command model is incorrect</response>
    /// <response code="404">Chat was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddMessage(string id, CreateMessageCommand command,
        CancellationToken cancellationToken) {
        
        await messageService.AddNewMessageAsync(ulong.Parse(id), command, cancellationToken);
        return Created();
    }
    
    /// <summary>
    /// Get all messages from a specific chat
    /// </summary>
    /// <param name="id">ID of a specific chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns all messages from a specific chat</response>
    /// <response code="400">Chat id is incorrect</response>
    /// <response code="404">Chat was not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MessageViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMessages(string id, CancellationToken cancellationToken)
        => Ok(await messageService.GetMessagesFromChatAsync(ulong.Parse(id), cancellationToken));

    
    /// <summary>
    /// Get random message from a specific chat
    /// </summary>
    /// <param name="id">ID of a specific chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns random message from a specific chat</response>
    /// <response code="400">Chat id is incorrect</response>
    /// <response code="404">Chat was not found</response>
    [HttpGet("/random")]
    [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRandomMessage(string id, CancellationToken cancellationToken)
        => Ok(await messageService.GetRandomMessageFromChatAsync(ulong.Parse(id), cancellationToken));

    
    /// <summary>
    /// Get all messages of a certain type from a specific chat
    /// </summary>
    /// <param name="id">ID of a specific chat</param>
    /// <param name="category">Type of message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns messages of a certain type from a specific chat</response>
    /// <response code="400">Chat id is incorrect</response>
    /// <response code="404">Chat was not found</response>
    [HttpGet("/{category}")]
    [CategoryValidationFilter]
    [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMessages(string id, string category, CancellationToken cancellationToken)
        => Ok(await messageService.GetMessagesFromChatAsync(
            ulong.Parse(id), Enum.Parse<ContentType>(category), cancellationToken));
    
    
    /// <summary>
    /// Get random message of a certain type from a specific chat
    /// </summary>
    /// <param name="id">ID of a specific chat</param>
    /// <param name="category">Type of message</param>
    /// <param name="cancellationToken">Chat id is incorrect</param>
    /// <response code="200">Returns random message of a certain type from a specific chat</response>
    /// <response code="400">Chat id is incorrect</response>
    /// <response code="404">Chat was not found</response>
    [HttpGet("/{category}/random")]
    [CategoryValidationFilter]
    [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRandomMessage(string id, string category, CancellationToken cancellationToken)
        => Ok(await messageService.GetRandomMessageFromChatAsync(
            ulong.Parse(id), Enum.Parse<ContentType>(category), cancellationToken));
}