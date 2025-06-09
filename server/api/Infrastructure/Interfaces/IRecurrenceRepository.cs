using server.Domain.Entities;

namespace server.Infrastructure.Interfaces;

public interface IRecurrenceRepository
{
    Task AddAsync(Recurrence recurrences);
}
