using api.Application.Transactions.Commands;
using api.Application.Transactions.Commands.Handlers;
using api.Application.Transactions.Dtos;
using api.Application.Transactions.Validators;
using api.Domain.Entities;
using api.Domain.Enums;
using api.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace UnitTest.Handler;

public class CreateTransactionHandlerTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPaymentMethodRepository> _paymentMethodRepository = new();
    private readonly Mock<ICategoryRepository> _categoryRepository = new();
    private readonly Mock<IRecurrenceRepository> _recurrenceRepository = new();
    private readonly Mock<ITransactionItemRepository> _transactionItemRepository = new();
    private readonly CreateTransactionHandler _handler;

    public CreateTransactionHandlerTests()
    {
        _handler = new CreateTransactionHandler(
            _transactionRepositoryMock.Object,
            _userRepositoryMock.Object,
            _paymentMethodRepository.Object,
            _categoryRepository.Object,
            _recurrenceRepository.Object,
            _transactionItemRepository.Object,
            new CreateTransactionCommandValidator()
        );
    }

    [Fact]
    public async Task Handle_should_throw_UnauthorizedAccessException_when_User_not_found()
    {
        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            Amount = 100m,
            PaymentMethod = PaymentMethodType.CreditCard
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(command.UserId)).ReturnsAsync((User?)null);

        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));

        ex.Message.Should().Be("User not found.");
    }

    [Fact]
    public async Task Handle_should_throw_ArgumentException_when_PaymentMethod_not_found()
    {
        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            Amount = 100m,
            PaymentMethod = PaymentMethodType.CreditCard
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(command.UserId)).ReturnsAsync(new User { UserId = command.UserId });

        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync((PaymentMethod?)null);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));

        ex.ParamName.Should().Be(nameof(CreateTransactionCommand.PaymentMethod));
        ex.Message.Should().StartWith("Invalid payment method.");
    }

    [Fact]
    public async Task Handle_should_create_transaction_successfully_with_minimum_data()
    {
        var userId = Guid.NewGuid();
        var paymentMethod = CreatePaymentMethod();

        var command = new CreateTransactionCommand
        {
            UserId = userId,
            Amount = 100m,
            PaymentMethod = PaymentMethodType.CreditCard,
            PaymentDate = DateTime.Today,
            Type = TransactionType.Expense,
            Status = TransactionStatus.Pending
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(new User { Id = 1, UserId = userId });
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync(paymentMethod);
        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);
        
        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Amount.Should().Be(100m);
        result.Id.Should().NotBeEmpty();

        _transactionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Once);
    }

    [Fact]
    public async Task Handle_should_create_Category_if_not_exists()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = 1, UserId = userId };
        var categoryName = "Alimentação";
        var paymentMethod = CreatePaymentMethod();

        var command = new CreateTransactionCommand
        {
            UserId = userId,
            Amount = 50,
            PaymentMethod = PaymentMethodType.CreditCard,
            Category = categoryName
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync(paymentMethod);
        _categoryRepository.Setup(x => x.GetByNameAndUserIdAsync(categoryName, user.Id)).ReturnsAsync((Category?)null);
        _categoryRepository.Setup(x => x.AddAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _categoryRepository.Verify(x => x.AddAsync(It.Is<Category>(c => c.Name == categoryName && c.UserId == user.Id)), Times.Once);
    }

    [Fact]
    public async Task Handle_should_not_create_Category_if_it_already_exists()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = 1, UserId = userId };
        var category = new Category { Id = 10, Name = "Alimentação", UserId = user.Id };
        var paymentMethod = CreatePaymentMethod();

        var command = new CreateTransactionCommand
        {
            UserId = userId,
            Amount = 50,
            PaymentMethod = PaymentMethodType.CreditCard,
            Category = "Alimentação"
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync(paymentMethod);
        _categoryRepository.Setup(x => x.GetByNameAndUserIdAsync(command.Category, user.Id)).ReturnsAsync(category);
        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _categoryRepository.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Never);
    }

    [Fact]
    public async Task Handle_should_create_Recurrence_if_provided()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = 1, UserId = userId };
        var paymentMethod = CreatePaymentMethod();

        var recurrenceInput = new RecurrenceInputDto
        {
            Frequency = RecurrenceFrequency.Monthly,
            Interval = 1,
            EndDate = DateTime.Today.AddMonths(6),
            NextDueDate = DateTime.Today.AddMonths(1)
        };

        var command = new CreateTransactionCommand
        {
            UserId = userId,
            Amount = 120,
            PaymentMethod = PaymentMethodType.CreditCard,
            Recurrence = recurrenceInput
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync(paymentMethod);
        _recurrenceRepository.Setup(x => x.AddAsync(It.IsAny<Recurrence>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _recurrenceRepository.Verify(x => x.AddAsync(It.Is<Recurrence>(r =>
            r.Frequency == recurrenceInput.Frequency &&
            r.Interval == recurrenceInput.Interval &&
            r.EndDate == recurrenceInput.EndDate &&
            r.NextDueDate == recurrenceInput.NextDueDate &&
            r.IsActive == true
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_should_not_create_Recurrence_if_null()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = 1, UserId = userId };
        var paymentMethod = CreatePaymentMethod();

        var command = new CreateTransactionCommand
        {
            UserId = userId,
            Amount = 50,
            PaymentMethod = PaymentMethodType.CreditCard,
            Recurrence = null
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync(paymentMethod);
        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _recurrenceRepository.Verify(x => x.AddAsync(It.IsAny<Recurrence>()), Times.Never);
    }

    [Fact]
    public async Task Handle_should_not_create_TransactionItems_if_items_is_null()
    {
        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            Amount = 50,
            PaymentMethod = PaymentMethodType.CreditCard,
            Items = null
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(command.UserId)).ReturnsAsync(new User { Id = 1, UserId = command.UserId });
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR"))
            .ReturnsAsync(CreatePaymentMethod());

        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _transactionItemRepository.Verify(x => x.AddAsync(It.IsAny<List<TransactionItem>>()), Times.Never);
    }

    [Fact]
    public async Task Handle_should_not_create_TransactionItems_if_items_is_empty()
    {
        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            Amount = 50,
            PaymentMethod = PaymentMethodType.CreditCard,
            Items = Array.Empty<TransactionItemInputDto>() // funciona fora do atributo
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(command.UserId)).ReturnsAsync(new User { Id = 1, UserId = command.UserId });
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR"))
            .ReturnsAsync(CreatePaymentMethod());

        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _transactionItemRepository.Verify(x => x.AddAsync(It.IsAny<List<TransactionItem>>()), Times.Never);
    }

    [Fact]
    public async Task Handle_should_map_items_correctly_to_TransactionItems()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = 1, UserId = userId };
        var paymentMethod = CreatePaymentMethod();

        var items = new List<TransactionItemInputDto>
        {
            new()
            {
                Name = "Produto A",
                Quantity = 2,
                UnitPrice = 10,
                UnitOfMeasure = "un"
            },
            new()
            {
                Name = "Produto B",
                Quantity = 1,
                UnitPrice = 20,
                UnitOfMeasure = "kg"
            }
        };

        var command = new CreateTransactionCommand
        {
            UserId = userId,
            Amount = 100,
            PaymentMethod = PaymentMethodType.CreditCard,
            Items = items
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
        _paymentMethodRepository.Setup(x => x.GetByTypeAndLocaleAsync(command.PaymentMethod, "pt-BR")).ReturnsAsync(paymentMethod);
        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        IList<TransactionItem> capturedItems = null!;
        _transactionItemRepository.Setup(x => x.AddAsync(It.IsAny<List<TransactionItem>>()))
            .Callback<IList<TransactionItem>>(items => capturedItems = items)
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        capturedItems.Should().HaveCount(2);

        capturedItems[0].Name.Should().Be("Produto A");
        capturedItems[0].Quantity.Should().Be(2);
        capturedItems[0].UnitPrice.Should().Be(10);
        capturedItems[0].UnitOfMeasure.Should().Be("un");

        capturedItems[1].Name.Should().Be("Produto B");
        capturedItems[1].Quantity.Should().Be(1);
        capturedItems[1].UnitPrice.Should().Be(20);
        capturedItems[1].UnitOfMeasure.Should().Be("kg");
    }


    private static PaymentMethod CreatePaymentMethod()
    {
        return new PaymentMethod
        {
            Id = 1,
            Name = "Cartão de crédito",
            Locale = "pt-BR",
            Identifier = PaymentMethodType.CreditCard,
        };
    }
}
