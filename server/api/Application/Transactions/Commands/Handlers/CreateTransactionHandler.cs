using FluentValidation;
using MediatR;
using api.Application.Transactions.Dtos;
using api.Domain.Entities;
using api.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace api.Application.Transactions.Commands.Handlers;

public class CreateTransactionHandler(
    ITransactionRepository repository, 
    IUserRepository userRepository, 
    IPaymentMethodRepository paymentMethodRepository,
    ICategoryRepository categoryRepository,
    IRecurrenceRepository recurrenceRepository,
    ITransactionItemRepository transactionItemRepository,
    IValidator<CreateTransactionCommand> validator
) : IRequestHandler<CreateTransactionCommand, TransactionResponseDto>
{
    private readonly ITransactionRepository _repository = repository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository = paymentMethodRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IRecurrenceRepository _recurrenceRepository = recurrenceRepository;
    private readonly ITransactionItemRepository _transactionItemRepository = transactionItemRepository;
    private readonly IValidator<CreateTransactionCommand> _validator = validator;

    public async Task<TransactionResponseDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }

        // TODO: Use claim locale instead of hardcoded "pt-BR", but it is implemented futurely in the project.
        var paymentMethod = await _paymentMethodRepository.GetByTypeAndLocaleAsync(request.PaymentMethod, "pt-BR");

        if (paymentMethod == null)
        {
            throw new ArgumentException("Invalid payment method.", nameof(request.PaymentMethod));
        }

        long? categoryId = null;

        if (request.Category != null)
        {
            var category = await _categoryRepository.GetByNameAndUserIdAsync(request.Category, user.Id);

            if (category == null)
            {
                category = new Category
                {
                    Name = request.Category,
                    UserId = user.Id,
                };

                await _categoryRepository.AddAsync(category);

            }

            categoryId = category.Id;
        }

        long? recurrenceId = null;

        if (request.Recurrence != null)
        {
            var recurrence = new Recurrence
            {
                RecurrenceId = Guid.NewGuid(),
                IsActive = true,
                Frequency = request.Recurrence.Frequency,
                Interval = request.Recurrence.Interval,
                EndDate = request.Recurrence.EndDate,
                NextDueDate = request.Recurrence.NextDueDate,
            };

            await _recurrenceRepository.AddAsync(recurrence);

            recurrenceId = recurrence.Id;
        }

        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            UserId = user.Id,
            Amount = request.Amount,
            CategoryId = categoryId,
            PaymentDate = request.PaymentDate,
            PaymentMethodId = paymentMethod.Id,
            Status = request.Status,
            TransactionType = request.Type,
            RecurrenceId = recurrenceId,
        };

        await _repository.AddAsync(transaction);

        if (request.Items != null && request.Items.Any())
        {
            var transactionItems = request.Items.Select(item => new TransactionItem
            {
                TransactionItemId = Guid.NewGuid(),
                Name = item.Name,
                Quantity = item.Quantity,
                UnitOfMeasure = item.UnitOfMeasure,
                UnitPrice = item.UnitPrice,
                TransactionId = transaction.Id,
            }).ToList();

            await _transactionItemRepository.AddAsync(transactionItems);
        }

        return new TransactionResponseDto
        {
            Id = transaction.TransactionId,
            Amount = transaction.Amount
        };
    }
}
