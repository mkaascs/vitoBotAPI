using Domain.Primitives;
using Domain.Entities.Enums;

namespace Domain.Entities;

public class Message : Entity {
    public required string Content { get; init; }
    
    public ContentType Type { get; init; }
}