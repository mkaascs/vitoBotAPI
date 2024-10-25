using FluentValidation;

namespace Application.DTO.Commands.Validation;

public class RegisterChatCommandValidator : AbstractValidator<RegisterChatCommand>
{
    public RegisterChatCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Name)
            .Must(name => name is not { Length: > 128 })
            .WithMessage("Name length must not exceed 128");
    }
}