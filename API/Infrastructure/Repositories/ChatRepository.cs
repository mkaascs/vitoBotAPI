using Domain.Entities;
using Domain.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository(ApplicationDbContext dbContext) : IChatRepository {
    public async Task<IEnumerable<Chat>> GetChatsAsync(CancellationToken cancellationToken=default) 
        => await dbContext.Chats.ToListAsync(cancellationToken);

    public async Task<Chat?> GetChatByIdAsync(ulong chatId, CancellationToken cancellationToken = default)
        => await dbContext.Chats.FirstOrDefaultAsync(chat => chat.Id.Equals(chatId), cancellationToken);

    public async Task<bool> DoesAlreadyExist(Chat chat, CancellationToken cancellationToken=default) {
        Chat? foundChat = await dbContext.Chats
            .FirstOrDefaultAsync(otherChat => chat.Id.Equals(otherChat.Id), cancellationToken);

        return foundChat != null;
    }

    public async Task CreateChatAsync(Chat newChat, CancellationToken cancellationToken=default) { 
        dbContext.Chats.Add(newChat);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}