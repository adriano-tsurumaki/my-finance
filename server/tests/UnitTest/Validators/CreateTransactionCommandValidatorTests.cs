using api.Application.Transactions.Commands;
using api.Application.Transactions.Dtos;
using api.Application.Transactions.Validators;
using api.Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTest.Validators;

public class CreateTransactionCommandValidatorTests
{
    private readonly CreateTransactionCommandValidator _validator = new();
    
    [Fact]
    public void Should_have_a_error_when_UserId_is_null()
    {
        var model = new CreateTransactionCommand();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UserId)
            .WithErrorMessage("User ID is required.");
    }

    [Fact]
    public void Should_not_have_error_when_UserId_is_valid()
    {
        var model = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid()
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_have_a_error_when_Amount_is_zero()
    {
        var model = new CreateTransactionCommand
        {
            Amount = 0m
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Amount)
            .WithErrorMessage("Amount must be greater than zero.");
    }

    [Fact]
    public void Should_not_have_error_when_Amount_is_greater_than_zero()
    {
        var model = new CreateTransactionCommand
        {
            Amount = 100m
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Amount);
    }

    [Fact]
    public void Should_have_a_error_when_PaymentDate_is_null()
    {
        var model = new CreateTransactionCommand
        {
            PaymentDate = default
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PaymentDate)
            .WithErrorMessage("Payment date is required.");
    }

    [Fact]
    public void Should_not_have_error_when_PaymentDate_is_valid()
    {
        var model = new CreateTransactionCommand
        {
            PaymentDate = DateTime.UtcNow
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PaymentDate);
    }

    [Fact]
    public void Should_have_a_error_when_Type_is_invalid()
    {
        var model = new CreateTransactionCommand
        {
            Type = (TransactionType)999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Type)
            .WithErrorMessage("Invalid transaction type.");
    }

    [Fact]
    public void Should_not_have_error_when_Type_is_valid()
    {
        var model = new CreateTransactionCommand
        {
            Type = TransactionType.Expense
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public void Should_have_a_error_when_Status_is_invalid()
    {
        var model = new CreateTransactionCommand
        {
            Status = (TransactionStatus)999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Invalid transaction status.");
    }

    [Fact]
    public void Should_not_have_error_when_Status_is_valid()
    {
        var model = new CreateTransactionCommand
        {
            Status = TransactionStatus.Pending
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Status);
    }

    [Fact]
    public void Should_have_a_error_when_PaymentMethod_is_invalid()
    {
        var model = new CreateTransactionCommand
        {
            PaymentMethod = (PaymentMethodType)999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PaymentMethod)
            .WithErrorMessage("Invalid payment method type.");
    }

    [Fact]
    public void Should_not_have_error_when_PaymentMethod_is_valid()
    {
        var model = new CreateTransactionCommand
        {
            PaymentMethod = PaymentMethodType.CreditCard
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PaymentMethod);
    }

    [Fact]
    public void Should_have_a_error_when_Category_exceeds_max_length()
    {
        var model = new CreateTransactionCommand
        {
            Category = new string('a', 101)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Category)
            .WithErrorMessage("Category name cannot exceed 100 characters.");
    }

    [Fact]
    public void Should_not_have_error_when_Category_is_valid()
    {
        var model = new CreateTransactionCommand
        {
            Category = "Valid Category"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Category);
    }

    [Fact]
    public void Should_have_error_when_Recurrence_is_not_null_and_invalid()
    {
        var model = new CreateTransactionCommand
        {
            Recurrence = new RecurrenceInputDto
            {
                Frequency = (RecurrenceFrequency)999,
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Recurrence.Frequency")
            .WithErrorMessage("Frequency must be a valid value.");
    }

    [Fact]
    public void Should_not_have_error_when_Recurrence_is_null()
    {
        var model = new CreateTransactionCommand();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Recurrence);
    }

    [Fact]
    public void Should_have_error_when_Items_is_empty()
    {
        var model = new CreateTransactionCommand
        {
            Items =
            [
                new()
                {
                    Quantity = -1
                }
            ]
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Items[0].Quantity")
            .WithErrorMessage("Quantity must be greater than zero.");
    }
}