using Domain.Entities;
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
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="Message"/> instances from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<IEnumerable<Message>> GetMessagesFromChatAsync(ulong chatId);

    /// <summary>
    /// Asynchronous method to obtain random instance of the <see cref="Message"/> class in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <returns>Returns a random instance of <see cref="Message"/> from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with the specific id doesn't exist</exception>
    Task<Message> GetRandomMessageFromChatAsync(ulong chatId);
    
    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Message"/> class
    /// </summary>
    /// <param name="newMessage">Instance of <see cref="Message"/> which need to create</param>
    /// <returns>Returns true if instance of <see cref="Message"/> was successfully added. Returns false if message with this content was already exist</returns>
    Task<bool> AddNewMessageAsync(Message newMessage);
}