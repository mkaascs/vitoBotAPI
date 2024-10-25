using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository(ApplicationDbContext dbContext) : IRepository<Chat>
{
    public DbSet<Chat> Entities { get; } = dbContext.Chats;
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}