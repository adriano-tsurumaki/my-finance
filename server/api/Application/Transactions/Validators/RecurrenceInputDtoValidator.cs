using FluentValidation;
using api.Application.Transactions.Dtos;

namespace api.Application.Transactions.Validators;

public class RecurrenceInputDtoValidator : AbstractValidator<RecurrenceInputDto?>
{
    public RecurrenceInputDtoValidator()
    {
        RuleFor(x => x!.IsActive)
            .NotNull()
            .WithMessage("IsActive must not be null.");

        RuleFor(x => x!.Frequency)
            .IsInEnum()
            .WithMessage("Frequency must be a valid value.");

        RuleFor(x => x!.Interval)
            .GreaterThan(0)
            .WithMessage("Interval must be greater than 0.");

        RuleFor(x => x!.NextDueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("NextDueDate must be greater than or equal to the current date and time.");
    }
}
