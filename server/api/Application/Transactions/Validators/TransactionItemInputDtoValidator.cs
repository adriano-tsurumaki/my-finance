using FluentValidation;
using api.Application.Transactions.Dtos;

namespace api.Application.Transactions.Validators;

public class TransactionItemInputDtoValidator : AbstractValidator<TransactionItemInputDto>
{
    public TransactionItemInputDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitOfMeasure)
            .NotEmpty().WithMessage("Unit of measure is required.")
            .MaximumLength(20).WithMessage("Unit of measure must not exceed 20 characters.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price must be zero or greater.");
    }
}
