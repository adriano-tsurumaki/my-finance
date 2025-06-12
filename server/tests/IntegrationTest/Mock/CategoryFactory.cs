using api.Domain.Entities;

namespace IntegrationTest.Mock;

public class CategoryFactory : IEntityFactory<Category>
{
    public Category Create()
    {
        return new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = "Shopping",
        };
    }
}
