using Domain.Exceptions;
using Domain.Entities.Enums;

using Application.Exceptions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;

namespace Application.Abstractions;

/// <summary>
/// Interface of Message Service which allows to interact with instances of <see cref="Domain.Entities.Message"/>
/// </summary>
public interface IMessageService {
    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="MessageViewModel"/> class in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="MessageViewModel"/> instances from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with current id doesn't exist</exception>
    Task<IEnumerable<MessageViewModel>> GetMessagesFromChatAsync(ulong chatId, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method to obtain instances of the <see cref="MessageViewModel"/> class of a certain type in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="contentType">Message type to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="MessageViewModel"/> instances of a certain type from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with current id doesn't exist</exception>
    Task<IEnumerable<MessageViewModel>> GetMessagesFromChatAsync(ulong chatId, ContentType contentType, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method to obtain random instance of the <see cref="MessageViewModel"/> class in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a random instance of <see cref="MessageViewModel"/> from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with current id doesn't exist</exception>
    Task<MessageViewModel> GetRandomMessageFromChatAsync(ulong chatId, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method to obtain random instance of the <see cref="MessageViewModel"/> class of a certain type in a specific chat
    /// </summary>
    /// <param name="chatId">Unique id of the specific chat</param>
    /// <param name="contentType">Message type to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns a random instance of <see cref="MessageViewModel"/> of a certain type from the specific chat</returns>
    /// <exception cref="ChatWasNotFoundException">Throws if chat with current id doesn't exist</exception>
    Task<MessageViewModel> GetRandomMessageFromChatAsync(ulong chatId, ContentType contentType, CancellationToken cancellationToken=default);

    /// <summary>
    /// Asynchronous method that adds a new instance of the <see cref="Domain.Entities.Message"/> class if there are no messages with this content and content type
    /// </summary>
    /// <param name="chatId">ID of the chat to add the message to</param>
    /// <param name="command">Instance of <see cref="CreateMessageCommand"/> which need to create an instance of <see cref="Domain.Entities.Message"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if message was added successfully or false if message with this content and content type already exists</returns>
    /// <exception cref="ValidationProblemException">Throws if command model is not valid</exception>
    Task<bool> AddNewMessageAsync(ulong chatId, CreateMessageCommand command, CancellationToken cancellationToken=default);
}