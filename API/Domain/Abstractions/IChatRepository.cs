using Domain.Entities;

namespace Domain.Abstractions;

/// <summary>
/// Interface, an abstraction layer that provides public methods for obtaining and creating instances of the <see cref="Chat"/> class
/// </summary>
public interface IChatRepository {
    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="Chat"/> class
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="Chat"/> instances</returns>
    Task<IEnumerable<Chat>> GetChatsAsync(CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method that checks if there is already a message with this id
    /// </summary>
    /// <param name="chat">Instance of <see cref="Chat"/>></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns true if chat with the id already exists and false if it doesn't</returns>
    Task<bool> DoesAlreadyExist(Chat chat, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Chat"/> class
    /// </summary>
    /// <param name="newChat">Instance of <see cref="Chat"/> which need to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task CreateChatAsync(Chat newChat,CancellationToken cancellationToken=default);
}