using Domain.Entities.Enums;
using FluentValidation;

namespace Application.DTO.Commands.Validation;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand> {
    public CreateMessageCommandValidator() {
        RuleFor(command => command.Content).NotEmpty();
        RuleFor(command => command.Type).Must(
            type => Enum.TryParse(typeof(ContentType), type, out _))
                .WithMessage("The value must be a valid Enum");
    }
}