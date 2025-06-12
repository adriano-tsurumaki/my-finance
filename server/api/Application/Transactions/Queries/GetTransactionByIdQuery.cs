using MediatR;
using api.Application.Transactions.Dtos;

namespace api.Application.Transactions.Queries;

public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionResponseDto>;
