using api.Application.Transactions.Dtos;
using api.Application.Transactions.Validators;
using api.Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTest.Validators;

public class RecurrenceInputDtoValidatorTests
{
    private readonly RecurrenceInputDtoValidator _validator = new();

    [Fact]
    public void Should_have_an_error_when_Frequency_is_invalid()
    {
        var model = new RecurrenceInputDto
        {
            Frequency = (RecurrenceFrequency)999,
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Frequency)
            .WithErrorMessage("Frequency must be a valid value.");
    }

    [Fact]
    public void Should_not_have_error_when_Frequency_is_valid()
    {
        var model = new RecurrenceInputDto
        {
            Frequency = RecurrenceFrequency.Monthly,
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Frequency);
    }

    [Fact]
    public void Should_have_an_error_when_Interval_is_zero_or_negative()
    {
        var model = new RecurrenceInputDto
        {
            Interval = 0,
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Interval)
            .WithErrorMessage("Interval must be greater than 0.");
    }

    [Fact]
    public void Should_not_have_error_when_Interval_is_positive()
    {
        var model = new RecurrenceInputDto
        {
            Interval = 1,
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Interval);
    }

    [Fact]
    public void Should_have_an_error_when_NextDueDate_is_in_the_past()
    {
        var model = new RecurrenceInputDto
        {
            NextDueDate = DateTime.UtcNow.AddDays(-1),
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.NextDueDate)
            .WithErrorMessage("NextDueDate must be greater than or equal to the current date and time.");
    }

    [Fact]
    public void Should_not_have_error_when_NextDueDate_is_in_the_future()
    {
        var model = new RecurrenceInputDto
        {
            NextDueDate = DateTime.UtcNow.AddDays(1),
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.NextDueDate);
    }
}
