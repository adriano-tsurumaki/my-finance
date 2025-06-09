using MediatR;
using server.Application.Transactions.Dtos;

namespace server.Application.Transactions.Queries;

public record GetAllTransactionsQuery() : IRequest<List<TransactionResponseDto>>;

