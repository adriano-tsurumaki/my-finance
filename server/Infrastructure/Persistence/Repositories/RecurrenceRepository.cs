using server.Domain.Entities;
using server.Infrastructure.Interfaces;
using server.Infrastructure.Persistence.Context;

namespace server.Infrastructure.Persistence.Repositories;

public class RecurrenceRepository(FinanceDbContext financeDbContext) : IRecurrenceRepository
{
    private readonly FinanceDbContext _financeDbContext = financeDbContext;

    public async Task AddAsync(Recurrence recurrences)
    {
        await _financeDbContext.Recurrences.AddAsync(recurrences);
        await _financeDbContext.SaveChangesAsync();
    }
}
