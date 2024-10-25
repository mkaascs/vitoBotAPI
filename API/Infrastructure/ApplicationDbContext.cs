using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Chat> Chats { get; init; }
    
    public DbSet<Message> Messages { get; init; }
}