
using api.Domain.Entities;
using api.Infrastructure.Persistence.Context;
using FluentAssertions;
using IntegrationTest.Mock;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTest;

[Collection("Integration Tests")]
public class TransactionPersistenceTests(TestContainerFixture fixture) : BasePersistenceTests
{
    private readonly TestContainerFixture _fixture = fixture;

    [Fact]
    public async Task Should_not_save_transaction_without_required_relationships()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            Amount = 100,
        };

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_saving_transaction_with_invalid_user()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transaction = Create<Transaction>();
        var paymentMethod = Create<PaymentMethod>();
        
        transaction.PaymentMethod = paymentMethod;

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_saving_transaction_with_invalid_payment_method()
    {
        using var context = await _fixture.CreateNewDbContext(); 

        var transaction = Create<Transaction>();
        var user = Create<User>();
        
        transaction.User = user;

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_payment_method_does_not_exist()
    {
        using var context = await _fixture.CreateNewDbContext();
        
        var transaction = Create<Transaction>();
        transaction.PaymentMethodId = -1;

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_installment_plan_does_not_exist()
    {
        using var context = await _fixture.CreateNewDbContext();
        
        var transaction = Create<Transaction>();
        transaction.InstallmentPlanId = -1;

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_recurrence_does_not_exist()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transaction = Create<Transaction>();
        transaction.RecurrenceId = -1;

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_category_does_not_exist()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transaction = Create<Transaction>();
        transaction.CategoryId = -1;

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_save_and_load_transaction_with_required_relationships()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transaction = Create<Transaction>();
        var user = Create<User>();
        var paymentMethod = Create<PaymentMethod>();

        transaction.User = user;
        transaction.PaymentMethod = paymentMethod;

        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        var savedTransaction = await context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transaction.TransactionId);
        savedTransaction.Should().NotBeNull();

        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        savedUser.Should().NotBeNull();

        var savedPaymentMethod = await context.PaymentMethods.FirstOrDefaultAsync(u => u.Id == paymentMethod.Id);
        savedPaymentMethod.Should().NotBeNull();
    }
}