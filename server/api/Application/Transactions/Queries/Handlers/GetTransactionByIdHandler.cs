using MediatR;
using api.Application.Exceptions;
using api.Application.Transactions.Dtos;
using api.Application.Transactions.Mappers;
using api.Repositories.Interfaces;

namespace api.Application.Transactions.Queries.Handlers;

public class GetTransactionByIdHandler(ITransactionRepository repository) : IRequestHandler<GetTransactionByIdQuery, TransactionResponseDto>
{
    private readonly ITransactionRepository _repository = repository;

    public async Task<TransactionResponseDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(request.Id);

        if (transaction != null)
        {
            return transaction.ToDto(); // usa mapper ou método manual
        }

        throw new NotFoundException($"Transaction with ID {request.Id} not found.");
    }
}
