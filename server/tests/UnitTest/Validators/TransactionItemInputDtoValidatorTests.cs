using api.Application.Transactions.Dtos;
using api.Application.Transactions.Validators;
using FluentValidation.TestHelper;

namespace UnitTest.Validators;

public class TransactionItemInputDtoValidatorTests
{
    private readonly TransactionItemInputDtoValidator _validator = new();

    [Fact]
    public void Should_have_an_error_when_Name_is_empty()
    {
        var model = new TransactionItemInputDto
        {
            Name = string.Empty,
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name is required.");
    }

    [Fact]
    public void Should_not_have_error_when_Name_is_valid()
    {
        var model = new TransactionItemInputDto
        {
            Name = "Valid Item Name",
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Shoud_have_an_error_when_Name_exceeds_max_length()
    {
        var model = new TransactionItemInputDto
        {
            Name = new string('a', 101),
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name must not exceed 100 characters.");
    }

    [Fact]
    public void Should_not_have_error_when_Name_is_within_max_length()
    {
        var model = new TransactionItemInputDto
        {
            Name = new string('a', 100),
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_have_error_when_Quantity_is_zero_or_negative()
    {
        var model = new TransactionItemInputDto
        {
            Quantity = 0,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity must be greater than zero.");
    }

    [Fact]
    public void Should_not_have_error_when_Quantity_is_positive()
    {
        var model = new TransactionItemInputDto
        {
            Quantity = 1,
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void Should_have_error_when_UnitOfMeasure_is_empty()
    {
        var model = new TransactionItemInputDto
        {
            UnitOfMeasure = string.Empty,
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasure)
            .WithErrorMessage("Unit of measure is required.");
    }

    [Fact]
    public void Should_not_have_error_when_UnitOfMeasure_is_valid()
    {
        var model = new TransactionItemInputDto
        {
            UnitOfMeasure = "kg",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.UnitOfMeasure);
    }

    [Fact]
    public void Should_have_error_when_UnitOfMeasure_exceeds_max_length()
    {
        var model = new TransactionItemInputDto
        {
            UnitOfMeasure = new string('a', 21),
        };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasure)

            .WithErrorMessage("Unit of measure must not exceed 20 characters.");
    }

    [Fact]
    public void Should_not_have_error_when_UnitOfMeasure_is_within_max_length()
    {
        var model = new TransactionItemInputDto
        {
            UnitOfMeasure = new string('a', 20),
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.UnitOfMeasure);
    }
}
