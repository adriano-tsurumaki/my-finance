using server.Infrastructure.Persistence.Repositories;
using server.Repositories.Interfaces;

namespace server;

public static class DependecyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
