using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;
using Application.Exceptions;
using Application.Extensions;

using Domain.Abstractions;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Exceptions;

namespace Application.Services;

public class MessageService(
    IRepository<Chat> chatRepository,
    IRepository<Message> messageRepository,
    IValidator<CreateMessageCommand> messageValidator) : IMessageService
{
    private readonly Random _randomizer = new();
    
    public async ValueTask<IEnumerable<MessageViewModel>> GetMessagesFromChatAsync(
        ulong chatId,
        CancellationToken cancellationToken = default)
    {
        return await messageRepository.Entities
            .Where(message => message.ChatId.Equals(chatId))
            .Select(message => message.ToViewModel())
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<IEnumerable<MessageViewModel>> GetMessagesFromChatAsync(
        ulong chatId,
        ContentType contentType,
        CancellationToken cancellationToken = default)
    {
        return await messageRepository.Entities
            .Where(message => message.ChatId.Equals(chatId) && 
                              message.Type.Equals(contentType))
            .Select(message => message.ToViewModel())
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<MessageViewModel?> GetRandomMessageFromChatAsync(
        ulong chatId,
        CancellationToken cancellationToken = default)
    {
        IQueryable<MessageViewModel> messages = messageRepository.Entities
            .Where(message => message.ChatId.Equals(chatId))
            .Select(message => message.ToViewModel());

        int randomIndex = _randomizer.Next(0, messages.Count());
        return await messages.ElementAtOrDefaultAsync(randomIndex, cancellationToken);
    }

    public async ValueTask<MessageViewModel?> GetRandomMessageFromChatAsync(
        ulong chatId,
        ContentType contentType,
        CancellationToken cancellationToken = default)
    {
        IQueryable<MessageViewModel> messages = messageRepository.Entities
            .Where(message => message.ChatId.Equals(chatId) &&
                              message.Type.Equals(contentType))
            .Select(message => message.ToViewModel());

        int randomIndex = _randomizer.Next(0, messages.Count());
        return await messages.ElementAtOrDefaultAsync(randomIndex, cancellationToken);
    }

    public async ValueTask<bool> AddNewMessageAsync(
        ulong chatId, 
        CreateMessageCommand command,
        CancellationToken cancellationToken=default)
    {
        ValidationResult validationResult = messageValidator.Validate(command);
        
        if (!validationResult.IsValid)
            throw new ValidationProblemException(validationResult.ToDictionary());

        bool doesChatExist = await chatRepository.Entities
            .AnyAsync(chat => chat.Id.Equals(chatId), cancellationToken);

        if (!doesChatExist)
            throw new ChatNotFoundException(chatId);

        bool doesMessageExist = await messageRepository.Entities
            .Where(message => message.ChatId.Equals(chatId) &&
                              message.Content.Equals(command.Content) &&
                              message.Type.Equals(command.Type))
            .Select(message => message.ToViewModel())
            .AnyAsync(cancellationToken);

        if (doesMessageExist)
            return false;

        messageRepository.Entities.Add(command.ToDomainModel(chatId));
        await messageRepository.SaveChangesAsync(cancellationToken);
        return !cancellationToken.IsCancellationRequested;
    }
}