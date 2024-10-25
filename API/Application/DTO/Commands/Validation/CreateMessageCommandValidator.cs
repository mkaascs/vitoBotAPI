using FluentValidation;

namespace Application.DTO.Commands.Validation;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand> 
{
    public CreateMessageCommandValidator() 
    {
        RuleFor(command => command.Content)
            .NotEmpty();

        RuleFor(command => command.Content.Length)
            .ExclusiveBetween(1, 4096)
            .WithMessage("Content length must not exceed 4096");
            
        RuleFor(command => command.Type)
            .IsInEnum()
            .WithMessage("The value must be a valid content type");
    }
}