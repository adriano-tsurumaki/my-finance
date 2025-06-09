using api.Application.Transactions.Dtos;
using api.Domain.Entities;

namespace api.Application.Transactions.Mappers;

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