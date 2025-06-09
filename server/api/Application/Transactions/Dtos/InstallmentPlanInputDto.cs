namespace server.Application.Transactions.Dtos;

public class InstallmentPlanInputDto
{
    public decimal TotalAmount { get; set; } = 1.0m;
    public int TotalInstallments { get; set; } = 2;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; } = DateTime.UtcNow.AddMonths(1);
    public int IntervalInMonths { get; set; } = 1;
}
