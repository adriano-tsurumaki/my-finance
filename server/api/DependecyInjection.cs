using api.Application.Auth.Commands;
using api.Application.Generic.Validators;
using api.Application.Transactions.Commands;
using api.Infrastructure.Interfaces;
using api.Infrastructure.Persistence.Interceptors;
using api.Infrastructure.Persistence.Repositories;
using api.Infrastructure.Security;

namespace api;

public static class DependecyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<AuditInterceptor>();

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRecurrenceRepository, RecurrenceRepository>();
        services.AddScoped<ITransactionItemRepository, TransactionItemRepository>();

        //Securities
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateTransactionCommand).Assembly);
            
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
