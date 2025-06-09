using server.Domain.Entities;
using server.Domain.Enums;

namespace server.Infrastructure.Interfaces;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByTypeAndLocaleAsync(PaymentMethodType identifier, string locale);
}
