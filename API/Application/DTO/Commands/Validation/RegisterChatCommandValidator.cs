using FluentValidation;

namespace Application.DTO.Commands.Validation;

public class RegisterChatCommandValidator : AbstractValidator<RegisterChatCommand> {
    public RegisterChatCommandValidator() {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}