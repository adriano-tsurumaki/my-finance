using api.Domain.Entities;
using api.Infrastructure.Interfaces;
using api.Infrastructure.Persistence.Context;

namespace api.Infrastructure.Persistence.Repositories;

public class RecurrenceRepository(FinanceDbContext financeDbContext) : IRecurrenceRepository
{
    private readonly FinanceDbContext _financeDbContext = financeDbContext;

    public async Task AddAsync(Recurrence recurrences)
    {
        await _financeDbContext.Recurrences.AddAsync(recurrences);
        await _financeDbContext.SaveChangesAsync();
    }
}
