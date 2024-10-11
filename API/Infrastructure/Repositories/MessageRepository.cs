using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Exceptions;

namespace Infrastructure.Repositories;

public class MessageRepository(ChatRepository chatRepository, ApplicationDbContext dbContext) : IMessageRepository {
    public async Task<IEnumerable<Message>> GetMessagesFromChatAsync(ulong chatId, CancellationToken cancellationToken=default) {
        Chat foundChat = await chatRepository.GetChatByIdAsync(chatId, cancellationToken)
            ?? throw new ChatWasNotFoundException(chatId);

        return foundChat.Messages;
    }

    public async Task<IEnumerable<Message>> GetMessagesFromChatAsync(ulong chatId, ContentType contentType,
        CancellationToken cancellationToken=default) {
        
        Chat foundChat = await chatRepository.GetChatByIdAsync(chatId, cancellationToken)
            ?? throw new ChatWasNotFoundException(chatId);

        return foundChat.Messages
            .Where(message => message.Type.Equals(contentType));
    }

    public async Task<Message> GetRandomMessageFromChatAsync(ulong chatId, CancellationToken cancellationToken=default) {
        Chat foundChat = await chatRepository.GetChatByIdAsync(chatId, cancellationToken)
            ?? throw new ChatWasNotFoundException(chatId);

        return GetRandomMessageFromEnumerable(foundChat.Messages);
    }

    public async Task<Message> GetRandomMessageFromChatAsync(ulong chatId, ContentType contentType,
        CancellationToken cancellationToken=default) {
        
        Chat foundChat = await chatRepository.GetChatByIdAsync(chatId, cancellationToken)
            ?? throw new ChatWasNotFoundException(chatId);

        return GetRandomMessageFromEnumerable(foundChat.Messages
            .Where(message => message.Type.Equals(contentType)));
    }

    public async Task<bool> DoesAlreadyExist(Message message, CancellationToken cancellationToken = default) {
        Chat foundChat = await chatRepository.GetChatByIdAsync(message.ChatId, cancellationToken)
            ?? throw new ChatWasNotFoundException(message.ChatId);

        Message? foundMessages = foundChat.Messages
            .SingleOrDefault(otherMessage => message.Content.Equals(otherMessage.Content)
                                   && message.Type.Equals(otherMessage.Type));

        return foundMessages != null;
    }

    public async Task AddNewMessageAsync(Message newMessage, CancellationToken cancellationToken=default) {
        Chat foundChat = await chatRepository.GetChatByIdAsync(newMessage.ChatId, cancellationToken)
            ?? throw new ChatWasNotFoundException(newMessage.ChatId);
        
        foundChat.Messages.Add(newMessage);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static Message GetRandomMessageFromEnumerable(IEnumerable<Message> messages) {
        Random random = new();
        IList<Message> messagesList = messages.ToList();
        return messagesList[random.Next(0, messagesList.Count)];
    }
}