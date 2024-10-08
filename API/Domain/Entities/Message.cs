using Domain.Entities.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class Message : Entity {
    public Guid ChatId { get; init; }
    
    public string? Content { get; init; }
    
    public ContentType Type { get; init; }
    
    public User? Sender { get; init; }
}