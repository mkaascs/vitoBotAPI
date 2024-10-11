using FluentValidation;
using FluentValidation.Results;

using Application.Abstractions;
using Application.DTO.Commands;
using Application.DTO.ViewModels;
using Application.Exceptions;
using Application.Extensions;

using Domain.Abstractions;

namespace Application.Services;

public class ChatService(IChatRepository chatRepository, IValidator<RegisterChatCommand> chatValidator) : IChatService {
    public async Task<IEnumerable<ChatViewModel>> GetChatsAsync(CancellationToken cancellationToken = default)
        => (await chatRepository.GetChatsAsync(cancellationToken))
            .Select(chat => chat.ToViewModel());

    public async Task<bool> RegisterNewChatAsync(RegisterChatCommand command, CancellationToken cancellationToken = default) {
        ValidationResult validationResult = await chatValidator
            .ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationProblemException(validationResult.ToDictionary());

        bool doesAlreadyExist = await chatRepository
            .DoesAlreadyExist(command.ToChat(), cancellationToken);

        if (doesAlreadyExist)
            return false;

        await chatRepository.CreateChatAsync(command.ToChat(), cancellationToken);
        return true;
    }
}