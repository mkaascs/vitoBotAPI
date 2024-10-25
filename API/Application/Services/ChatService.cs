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
using Domain.Exceptions;

namespace Application.Services;

public class ChatService(
    IRepository<Chat> chatRepository,
    IValidator<RegisterChatCommand> chatValidator) : IChatService
{
    public async ValueTask<IEnumerable<ChatViewModel>> GetChatsAsync(CancellationToken cancellationToken = default)
    {
        return await chatRepository.Entities
            .Select(chat => chat.ToViewModel())
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<ChatViewModel> GetChatByIdAsync(ulong chatId, CancellationToken cancellationToken = default)
    {
        ChatViewModel? foundChat = await chatRepository.Entities
            .Where(chat => chat.Id.Equals(chatId))
            .Select(chat => chat.ToViewModel())
            .FirstOrDefaultAsync(cancellationToken);

        return foundChat ?? throw new ChatNotFoundException(chatId);
    }

    public async ValueTask<bool> RegisterNewChatAsync(RegisterChatCommand command, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = chatValidator.Validate(command);

        if (!validationResult.IsValid)
            throw new ValidationProblemException(validationResult.ToDictionary());

        bool doesExist = await chatRepository.Entities
            .AnyAsync(chat => chat.Id.Equals(command.Id), cancellationToken);

        if (doesExist) 
            return false;

        chatRepository.Entities.Add(command.ToDomainModel());
        await chatRepository.SaveChangesAsync(cancellationToken);
        return !cancellationToken.IsCancellationRequested;
    }
}