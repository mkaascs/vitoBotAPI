using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Exceptions;

namespace Domain.Abstractions;

/// <summary>
/// Interface, an abstraction layer that provides public methods for obtaining and creating instances of the <see cref="Message"/> class
/// </summary>
public interface IMessageRepository {
    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="Message"/> class in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="Message"/> instances from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<IEnumerable<Message>> GetMessagesFromChatAsync(ulong chatId, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="Message"/> class of a certain type in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="contentType">Type of messages to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="Message"/> instances of a certain type from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<IEnumerable<Message>> GetMessagesFromChatAsync(ulong chatId, ContentType contentType, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method to obtain random instance of the <see cref="Message"/> class in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a random instance of <see cref="Message"/> from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<Message> GetRandomMessageFromChatAsync(ulong chatId, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method to obtain random instance of the <see cref="Message"/> class of a certain type in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="contentType">Type of message to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a random instance of <see cref="Message"/> of a certain type from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<Message> GetRandomMessageFromChatAsync(ulong chatId, ContentType contentType, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method that checks if there is already a message with this content and content type in the chat
    /// </summary>
    /// <param name="message">Instance of <see cref="Message"/>></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns true if message with the content and content type already exists in the chat and false if it doesn't</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<bool> DoesAlreadyExist(Message message, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Message"/> class
    /// </summary>
    /// <param name="newMessage">Instance of <see cref="Message"/> which need to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task AddNewMessageAsync(Message newMessage, CancellationToken cancellationToken=default);
}