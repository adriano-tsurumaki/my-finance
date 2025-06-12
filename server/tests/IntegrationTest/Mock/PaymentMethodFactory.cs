using api.Domain.Entities;
using api.Domain.Enums;

namespace IntegrationTest.Mock;

public class PaymentMethodFactory : IEntityFactory<PaymentMethod>
{
    public PaymentMethod Create()
    {
        return new PaymentMethod
        {
            PaymentMethodId = Guid.NewGuid(),
            Name = "Credit Card",
            Locale = "en-US",
            Identifier = PaymentMethodType.CreditCard,
        };
    }
}
