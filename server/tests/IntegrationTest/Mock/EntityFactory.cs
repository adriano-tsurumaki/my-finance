using api.Domain.Entities;

namespace IntegrationTest.Mock;

public static class EntityFactory
{
    public static IEntityFactory<T> Generate<T>() where T : class, new()
    {
        return typeof(T) switch
        {
            Type t when t == typeof(User) => (IEntityFactory<T>)new UserFactory(),
            Type t when t == typeof(Category) => (IEntityFactory<T>)new CategoryFactory(),
            Type t when t == typeof(Transaction) => (IEntityFactory<T>)new TransactionFactory(),
            Type t when t == typeof(PaymentMethod) => (IEntityFactory<T>)new PaymentMethodFactory(),
            Type t when t == typeof(InstallmentPlan) => (IEntityFactory<T>)new InstallmentPlanFactory(),
            Type t when t == typeof(TransactionItem) => (IEntityFactory<T>)new TransactionItemFactory(),
            _ => throw new NotSupportedException($"No factory available for type {typeof(T).Name}")
        };
    }
}
