using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;
using server.Domain.Enums;
using server.Infrastructure.Interfaces;
using server.Infrastructure.Persistence.Context;

namespace server.Infrastructure.Persistence.Repositories;

public class PaymentMethodRepository(FinanceDbContext financeDbContext) : IPaymentMethodRepository
{
    private readonly FinanceDbContext _financeDbContext = financeDbContext;

    public Task<PaymentMethod?> GetByTypeAndLocaleAsync(PaymentMethodType identifier, string locale)
    {
        return _financeDbContext.PaymentMethods.FirstOrDefaultAsync(p => p.Identifier == identifier && p.Locale == locale);
    }

    // public async Task AddAsync(PaymentMethod paymentMethod)
    // {
    //     await _financeDbContext.PaymentMethods.AddAsync(paymentMethod);
    //     await _financeDbContext.SaveChangesAsync();
    // }
}
