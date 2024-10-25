using Domain.Entities;

using Application.DTO.ViewModels;
using Application.DTO.Commands;

namespace Application.Extensions;

internal static class DtoExtensions 
{
    public static MessageViewModel ToViewModel(this Message message)
        => new(message.Content, message.Type.ToString());
    
    public static ChatViewModel ToViewModel(this Chat chat)
        => new(chat.Id, chat.Name);
    
    public static Message ToDomainModel(this CreateMessageCommand command, ulong chatId)
        => new()
        {
            ChatId = chatId,
            Content = command.Content,
            Type = command.Type
        };

    public static Chat ToDomainModel(this RegisterChatCommand command)
        => new() 
        {
            Id = command.Id,
            Name = command.Name
        };
}