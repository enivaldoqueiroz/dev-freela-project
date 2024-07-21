using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";

        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        #region Consulmindo api do MS de Payment sem o RibbitMQ
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly string _paymentsBaseUrl;

        //public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        //}

        //public async Task<bool> ProcessPaymentViaMSPayment(PaymentInfoDto paymentInfoDto)
        //{
        //    // Implementar lógica de pagamento com Gateway de Pagamento

        //    var url = $"{_paymentsBaseUrl}/api/payments";
        //    var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);

        //    var paymentInfoContent = new StringContent(
        //        paymentInfoJson,
        //        Encoding.UTF8,
        //        "application/json"
        //        );

        //    var httpClient = _httpClientFactory.CreateClient("Payments");

        //    var response = await httpClient.PostAsync(url, paymentInfoContent);

        //    return response.IsSuccessStatusCode;
        //}
        #endregion

        public async Task ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            string paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);

            byte[] paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
