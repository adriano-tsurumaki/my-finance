using MediatR;
using server.Application.Transactions.Dtos;
using server.Domain.Enums;

namespace server.Application.Transactions.Commands;

public record class CreateTransactionCommand : IRequest<TransactionResponseDto>
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; } = 0.0m;
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public TransactionType Type { get; set; } = TransactionType.Expense;
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    public PaymentMethodType PaymentMethod { get; set; } = PaymentMethodType.CreditCard;
    public string? Category { get; set; }
    public RecurrenceInputDto? Recurrence { get; set; }
    public InstallmentPlanInputDto? InstallmentPlan { get; set; }
    public IList<TransactionItemInputDto>? Items { get; set; }
}
