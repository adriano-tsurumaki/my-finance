using api.Domain.Entities;

namespace api.Infrastructure.Interfaces;

public interface IRecurrenceRepository
{
    Task AddAsync(Recurrence recurrences);
}
