using FluentValidation;

using Domain.Entities.Enums;

using Application.DTO.Commands;

namespace Presentation.Validation;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand> {
    public CreateMessageCommandValidator() {
        RuleFor(command => command.Content).NotEmpty();
        RuleFor(command => command.Type).Must(
            type => Enum.TryParse(typeof(ContentType), type, out _))
                .WithMessage("The value must be a valid content type");
    }
}