using Application.Exceptions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;
using Domain.Exceptions;

namespace Application.Abstractions;

/// <summary>
/// Interface of Message Service which allows to interact with instances of <see cref="Domain.Entities.Chat"/>
/// </summary>
public interface IChatService 
{    
    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="ChatViewModel"/> class
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="ChatViewModel"/> instances</returns>
    ValueTask<IEnumerable<ChatViewModel>> GetChatsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronous method to obtain instance of the <see cref="ChatViewModel"/> class by id
    /// </summary>
    /// <param name="chatId">Unique id of chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="ChatViewModel"/> instance</returns>
    /// <exception cref="ChatNotFoundException">Throws if chat with this id does not exist</exception>
    ValueTask<ChatViewModel> GetChatByIdAsync(ulong chatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Domain.Entities.Chat"/> class if there are no chats with this id
    /// </summary>
    /// <param name="command">Instance of <see cref="RegisterChatCommand"/> which need to create an instance of <see cref="Domain.Entities.Chat"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if chat was registered successfully or false if chat with this id already exists</returns>
    /// <exception cref="ValidationProblemException">Throws if command model is not valid</exception>
    ValueTask<bool> RegisterNewChatAsync(RegisterChatCommand command, CancellationToken cancellationToken = default);
}