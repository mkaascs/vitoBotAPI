namespace Application.DTO.Commands;

public record CreateMessageCommand(
    string Content,
    string Type);