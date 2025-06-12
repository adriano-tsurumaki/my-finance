using api.Infrastructure.Persistence.Context;
using IntegrationTest.Mock;

namespace IntegrationTest;

public abstract class BasePersistenceTests
{
    internal static T Create<T>() where T : class, new() => EntityFactory.Generate<T>().Create();

    internal static Func<Task> SaveFn(FinanceDbContext context) => () => context.SaveChangesAsync();
}
