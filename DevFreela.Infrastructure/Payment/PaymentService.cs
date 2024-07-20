using DevFreela.Core.DTOs;
using DevFreela.Core.Services;

namespace DevFreela.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            // Implementar lógica de pagamento com Gateway de Pagamento

            return Task.FromResult(true);
        }
    }
}
