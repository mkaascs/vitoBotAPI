using FluentValidation;

using Application.DTO.Commands;

namespace Presentation.Validation;

public class RegisterChatCommandValidator : AbstractValidator<RegisterChatCommand> {
    public RegisterChatCommandValidator() {
        RuleFor(command => command.Id)
            .NotEmpty()
            .GreaterThan(0ul);
    }
}