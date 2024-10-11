using Domain.Entities;
using Domain.Entities.Enums;

using Application.DTO.ViewModels;
using Application.DTO.Commands;

namespace Application.Extensions;

internal static class DtoExtensions {
    public static MessageViewModel ToViewModel(this Message message)
        => new(message.Content,
            message.Type.ToString());
    
    public static Message ToMessage(this CreateMessageCommand command, ulong chatId)
        => new() {
            ChatId = chatId,
            Content = command.Content,
            Type = Enum.Parse<ContentType>(command.Type, true)
        };

    public static ChatViewModel ToViewModel(this Chat chat)
        => new(chat.Id, chat.Name);

    public static Chat ToChat(this RegisterChatCommand command)
        => new() {
            Id = command.Id,
            Name = command.Name
        };
}