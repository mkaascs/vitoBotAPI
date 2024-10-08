using Domain.Entities;

namespace Domain.Abstractions;

/// <summary>
/// Interface, an abstraction layer that provides public methods for obtaining and creating instances of the <see cref="Chat"/> class
/// </summary>
public interface IChatRepository {
    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="Chat"/> class
    /// </summary>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="Chat"/> instances</returns>
    Task<IEnumerable<Chat>> GetChatsAsync();

    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Chat"/> class
    /// </summary>
    /// <param name="newChat">Instance of <see cref="Chat"/> which need to create</param>
    /// <returns>Returns true if instance of <see cref="Chat"/> was successfully added. Returns false if chat was already exist</returns>
    Task<bool> CreateChatAsync(Chat newChat);
}