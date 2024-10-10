using System.ComponentModel.DataAnnotations;

namespace Domain.Primitives;

public abstract class Entity {
    [Key]
    public ulong Id { get; init; }
}