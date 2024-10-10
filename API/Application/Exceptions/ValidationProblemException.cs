namespace Application.Exceptions;

public class ValidationProblemException(IDictionary<string, string[]> problems) {
    /// <summary>
    /// Dictionary of validation problems
    /// </summary>
    private IDictionary<string, string[]> Problems { get; init; } = problems;
}