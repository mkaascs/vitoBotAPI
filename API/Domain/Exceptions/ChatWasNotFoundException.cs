namespace Domain.Exceptions;

public class ChatWasNotFoundException(Guid desiredChatId) : Exception {
    /// <summary>
    /// ID of the chat you are looking for, which does not exist
    /// </summary>
    public Guid DesiredChatId { get; init; } = desiredChatId;
}