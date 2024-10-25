using Microsoft.EntityFrameworkCore;

namespace Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : class
{
    public DbSet<TEntity> Entities { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}