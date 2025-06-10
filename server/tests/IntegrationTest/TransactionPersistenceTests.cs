
using api.Domain.Entities;
using api.Infrastructure.Persistence.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace IntegrationTest;

public class TransactionPersistenceTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _pgContainer;
    private FinanceDbContext _context = null!;

    public TransactionPersistenceTests()
    {
        _pgContainer = new PostgreSqlBuilder()
            .WithDatabase("myfinance_test")
            .WithUsername("postgres")
            .WithPassword("123456")
            .WithCleanUp(true) // destroi após o uso
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _pgContainer.StartAsync();

        var options = new DbContextOptionsBuilder<FinanceDbContext>()
            .UseNpgsql(_pgContainer.GetConnectionString())
            .Options;

        _context = new FinanceDbContext(options);

        await _context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _pgContainer.DisposeAsync();
    }


    [Fact]
    public async Task Should_Save_Transaction_With_Items()
    {
        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            Amount = 100,
            TransactionItems =
            [
                new() { Name = "Item A", Quantity = 1, UnitOfMeasure = "un" }
            ]
        };

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        var loaded = await _context.Transactions.Include(t => t.TransactionItems)
            .FirstOrDefaultAsync(t => t.TransactionId == transaction.TransactionId);

        loaded.Should().NotBeNull();
        loaded!.TransactionItems.Should().HaveCount(1);
    }
}