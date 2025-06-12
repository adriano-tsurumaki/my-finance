using MediatR;
using api.Application.Transactions.Dtos;

namespace api.Application.Transactions.Commands;

public record UpdateTransactionCommand(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    Guid? CategoryId
) : IRequest<TransactionResponseDto>;
