namespace Application.DTO.Commands;

public record RegisterChatCommand(
    ulong Id,
    string? Name);