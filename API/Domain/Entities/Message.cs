using System.ComponentModel.DataAnnotations;
using Domain.Primitives;
using Domain.Entities.Enums;

namespace Domain.Entities;

public class Message : Entity
{
    [Required]
    public ulong ChatId { get; init; }
    
    [Required]
    [MaxLength(4096)]
    public required string Content { get; init; }
    
    [Required]
    public ContentType Type { get; init; }
}