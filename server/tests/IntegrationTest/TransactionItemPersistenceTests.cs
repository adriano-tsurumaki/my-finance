using api.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTest;

[Collection("Integration Tests")]

public class TransactionItemPersistenceTests(TestContainerFixture fixture) : BasePersistenceTests
{
    private readonly TestContainerFixture _fixture = fixture;

    [Fact]
    public async Task Should_throw_when_transaction_item_has_invalid_transaction()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transactionItem = Create<TransactionItem>();
        transactionItem.TransactionId = -1;

        await context.TransactionItems.AddAsync(transactionItem);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_throw_when_transaction_item_has_invalid_installment_plan()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transactionItem = Create<TransactionItem>();
        transactionItem.InstallmentPlanId = -1;

        await context.TransactionItems.AddAsync(transactionItem);
        await SaveFn(context).Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_save_transaction_with_multiple_items()
    {
        using var context = await _fixture.CreateNewDbContext();

        var transaction = Create<Transaction>();
        var user = Create<User>();
        var paymentMethod = Create<PaymentMethod>();

        transaction.User = user;
        transaction.PaymentMethod = paymentMethod;
        transaction.TransactionItems = [];

        for (int i = 0; i < 10; i++)
        {
            transaction.TransactionItems.Add(Create<TransactionItem>());
        }

        await context.Transactions.AddAsync(transaction);
        await SaveFn(context).Should().NotThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Should_save_installment_plan_with_multiple_items()
    {
        using var context = await _fixture.CreateNewDbContext();

        var installmentPlan = Create<InstallmentPlan>();
        installmentPlan.TransactionItems = [];

        for (int i = 0; i < 10; i++)
        {
            installmentPlan.TransactionItems.Add(Create<TransactionItem>());
        }

        await context.InstallmentPlans.AddAsync(installmentPlan);
        await SaveFn(context).Should().NotThrowAsync<DbUpdateException>();
    }
}
