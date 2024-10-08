using Domain.Primitives;

namespace Domain.Entities;

public class User : Entity {
    public string? Name { get; init; }
    
    public string? Nickname { get; init; }
}