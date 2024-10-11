namespace Domain.Exceptions;

public class ChatWasNotFoundException(ulong desiredChatId) : Exception {
    /// <summary>
    /// ID of the chat you are looking for, which does not exist
    /// </summary>
    public ulong DesiredChatId { get; init; } = desiredChatId;
}