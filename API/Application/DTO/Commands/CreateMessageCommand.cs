using Domain.Entities.Enums;

namespace Application.DTO.Commands;

public record CreateMessageCommand(
    string Content,
    ContentType Type);