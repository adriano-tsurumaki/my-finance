using MediatR;

namespace api.Application.Transactions.Commands;

public record DeleteTransactionCommand(Guid Id) : IRequest;
