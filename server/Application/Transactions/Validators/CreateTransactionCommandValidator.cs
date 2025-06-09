using FluentValidation;
using server.Application.Transactions.Commands;

namespace server.Application.Transactions.Validators;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.PaymentDate)
            .NotEmpty().WithMessage("Payment date is required.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid transaction status.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method type.");

        RuleFor(x => x.Category)
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Category));

        RuleFor(x => x.Recurrence)
            .SetValidator(new RecurrenceInputDtoValidator())
            .When(x => x.Recurrence != null);

        RuleForEach(x => x.Items)
            .SetValidator(new TransactionItemInputDtoValidator())
            .When(x => x.Items != null && x.Items.Count > 0);
    }
}
