namespace Application.Exceptions;

public class ValidationProblemException(IDictionary<string, string[]> errors) : Exception {
    public IDictionary<string, string[]> Errors { get; init; } = errors;
}