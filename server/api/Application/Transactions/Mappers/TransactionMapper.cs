using server.Application.Transactions.Dtos;
using server.Domain.Entities;

namespace server.Application.Transactions.Mappers;

public static class TransactionMapper
{
    public static TransactionResponseDto ToDto(this Transaction transaction)
    {
        return new TransactionResponseDto
        {
            Id = transaction.TransactionId,
            Amount = transaction.Amount,
        };
    }
}