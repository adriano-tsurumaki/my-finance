using MediatR;
using server.Application.Transactions.Dtos;

namespace server.Application.Transactions.Commands;

public record UpdateTransactionCommand(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    Guid? CategoryId
) : IRequest<TransactionResponseDto>;
