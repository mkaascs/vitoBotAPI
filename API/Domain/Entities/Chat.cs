using System.ComponentModel.DataAnnotations;
using Domain.Primitives;

namespace Domain.Entities;

public class Chat : Entity
{
    [MaxLength(128)]
    public string? Name { get; init; }

    [Required]
    public ICollection<Message> Messages { get; init; } = [];
}