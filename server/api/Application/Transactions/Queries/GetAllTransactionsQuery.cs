using MediatR;
using api.Application.Transactions.Dtos;

namespace api.Application.Transactions.Queries;

public record GetAllTransactionsQuery() : IRequest<List<TransactionResponseDto>>;

