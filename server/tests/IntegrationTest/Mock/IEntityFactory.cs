namespace IntegrationTest.Mock;

public interface IEntityFactory<T> where T : class, new()
{
    T Create();
}
