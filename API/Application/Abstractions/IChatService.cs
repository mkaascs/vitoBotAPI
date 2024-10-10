using Application.DTO.Commands;
using Application.DTO.ViewModels;

namespace Application.Abstractions;

/// <summary>
/// Interface of Message Service which allows to interact with instances of <see cref="Domain.Entities.Chat"/>
/// </summary>
public interface IChatService {    
    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="ChatViewModel"/> class
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="ChatViewModel"/> instances</returns>
    Task<IEnumerable<ChatViewModel>> GetChatsAsync(CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Domain.Entities.Chat"/> class if there are no chats with this id
    /// </summary>
    /// <param name="command">Instance of <see cref="RegisterChatCommand"/> which need to create an instance of <see cref="Domain.Entities.Chat"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if chat was registered successfully or false if chat with this id already exists</returns>
    Task<bool> RegisterNewChatAsync(RegisterChatCommand command, CancellationToken cancellationToken=default);
}