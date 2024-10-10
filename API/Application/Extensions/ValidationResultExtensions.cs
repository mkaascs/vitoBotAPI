using FluentValidation.Results;

namespace Application.Extensions;

internal static class ValidationResultExtensions {
    public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
        => validationResult.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(error => error.Key,
                error => error.Select(fail => fail.ErrorMessage).ToArray());
}