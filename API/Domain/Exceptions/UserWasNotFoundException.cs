namespace Domain.Exceptions;

public class UserWasNotFoundException(Guid desiredUserId) : Exception {
    /// <summary>
    /// ID of the user you are looking for, which does not exist
    /// </summary>
    public Guid DesiredUserId { get; init; } = desiredUserId;
}