
using api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace IntegrationTest;

[CollectionDefinition("Integration Tests")]
public class IntegrationTestCollection : ICollectionFixture<TestContainerFixture> { }

public class TestContainerFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;
    public FinanceDbContext DbContext { get; private set; } = null!;

    public TestContainerFixture()
    {
        _container = new PostgreSqlBuilder()
            .WithName("myfinance_test_container")
            .WithDatabase("myfinance_test")
            .WithUsername("postgres")
            .WithPassword("123456")
            .WithCleanUp(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<FinanceDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        DbContext = new FinanceDbContext(options);

        await DbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }

    /// <summary>
    /// Ensure a fresh context for each test
    /// </summary>
    public async Task<FinanceDbContext> CreateNewDbContext()
    {
        var options = new DbContextOptionsBuilder<FinanceDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        var dbContext = new FinanceDbContext(options);

        await dbContext.Database.EnsureCreatedAsync();

        return dbContext;
    }
}