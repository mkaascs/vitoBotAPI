using Domain.Primitives;
using Domain.Entities.Enums;

namespace Domain.Entities;

public class Message : Entity {
    public string? Content { get; init; }
    
    public ContentType Type { get; init; }
}