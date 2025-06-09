using api.Domain.Entities;
using api.Domain.Enums;

namespace api.Infrastructure.Interfaces;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByTypeAndLocaleAsync(PaymentMethodType identifier, string locale);
}
