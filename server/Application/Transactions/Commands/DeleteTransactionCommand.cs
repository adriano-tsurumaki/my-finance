using MediatR;

namespace server.Application.Transactions.Commands;

public record DeleteTransactionCommand(Guid Id) : IRequest;
