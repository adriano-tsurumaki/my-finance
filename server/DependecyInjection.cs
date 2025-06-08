using server.Application.Auth.Commands;
using server.Application.Transactions.Commands;
using server.Infrastructure.Interfaces;
using server.Infrastructure.Persistence.Interceptors;
using server.Infrastructure.Persistence.Repositories;
using server.Infrastructure.Security;
using server.Repositories.Interfaces;

namespace server;

public static class DependecyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<AuditInterceptor>();

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        //Securities
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTransactionCommand).Assembly));

        return services;
    }
}
