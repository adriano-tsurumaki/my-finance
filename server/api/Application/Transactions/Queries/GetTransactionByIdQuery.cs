using MediatR;
using server.Application.Transactions.Dtos;

namespace server.Application.Transactions.Queries;

public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionResponseDto>;
