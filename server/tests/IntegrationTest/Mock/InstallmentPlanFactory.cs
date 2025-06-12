using api.Domain.Entities;

namespace IntegrationTest.Mock;

public class InstallmentPlanFactory : IEntityFactory<InstallmentPlan>
{
    public InstallmentPlan Create()
    {
        return new InstallmentPlan
        {
            InstallmentPlanId = Guid.NewGuid(),
            TotalAmount = 1000.00m,
            TotalInstallments = 12,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(12),
        };
    }
}
