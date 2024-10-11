using FluentValidation;
using FluentValidation.Results;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;
using Application.Exceptions;
using Application.Extensions;

using Domain.Abstractions;
using Domain.Entities.Enums;

namespace Application.Services;

public class MessageService(IMessageRepository messageRepository, IValidator<CreateMessageCommand> messageValidator) : IMessageService {
    public async Task<IEnumerable<MessageViewModel>> GetMessagesFromChatAsync(ulong chatId,
        CancellationToken cancellationToken=default)
        => (await messageRepository.GetMessagesFromChatAsync(chatId, cancellationToken))
            .Select(message => message.ToViewModel());

    public async Task<IEnumerable<MessageViewModel>> GetMessagesFromChatAsync(ulong chatId, ContentType contentType,
        CancellationToken cancellationToken=default)
        => (await messageRepository.GetMessagesFromChatAsync(chatId, contentType, cancellationToken))
            .Select(message => message.ToViewModel());

    public async Task<MessageViewModel> GetRandomMessageFromChatAsync(ulong chatId,
        CancellationToken cancellationToken=default)
        => (await messageRepository.GetRandomMessageFromChatAsync(chatId, cancellationToken))
            .ToViewModel();

    public async Task<MessageViewModel> GetRandomMessageFromChatAsync(ulong chatId, ContentType contentType,
        CancellationToken cancellationToken=default)
        => (await messageRepository.GetRandomMessageFromChatAsync(chatId, contentType, cancellationToken))
            .ToViewModel();

    public async Task<bool> AddNewMessageAsync(ulong chatId, CreateMessageCommand command,
        CancellationToken cancellationToken=default) {
        
        ValidationResult validationResult = await messageValidator
            .ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationProblemException(validationResult.ToDictionary());
        
        bool doesAlreadyExist = await messageRepository
            .DoesAlreadyExist(command.ToMessage(chatId), cancellationToken);

        if (doesAlreadyExist)
            return false;

        await messageRepository.AddNewMessageAsync(command.ToMessage(chatId), cancellationToken);
        return true;
    }
}