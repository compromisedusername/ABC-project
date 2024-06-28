using ABC.Repositories.Contracts;
using ABC.Repositories.Payment;

namespace ABC.Services.Revenue;

public class RevenueService : IRevenueService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IContractsRepository _contractsRepository;

        public RevenueService(IPaymentsRepository paymentsRepository, IContractsRepository contractsRepository)
        {
            _paymentsRepository = paymentsRepository;
            _contractsRepository = contractsRepository;
        }

        public async Task<decimal> GetCurrentRevenueAsync(string currency)
        {
            var totalPayments = await _paymentsRepository.GetTotalPaymentsAsync();
            return await ConvertToCurrency(totalPayments, currency);
        }

        public async Task<decimal> GetCurrentRevenueByProductAsync(int productId, string currency)
        {
            var totalPayments = await _paymentsRepository.GetTotalPaymentsByProductAsync(productId);
            return await ConvertToCurrency(totalPayments, currency);
        }

        public async Task<decimal> GetPredictedRevenueAsync(string currency)
        {
            var totalRevenue = await _contractsRepository.GetPredictedRevenueAsync();
            return await ConvertToCurrency(totalRevenue, currency);
        }

        public async Task<decimal> GetPredictedRevenueByProductAsync(int productId, string currency)
        {
            var totalRevenue = await _contractsRepository.GetPredictedRevenueByProductAsync(productId);
            return await ConvertToCurrency(totalRevenue, currency);
        }

        private async Task<decimal> ConvertToCurrency(decimal amount, string currency)
        {
            if (currency == "PLN")
            {
                return amount;
            }

            using var client = new HttpClient();
            var response = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/PLN");
            var rates = 1;//JObject.Parse(response)["rates"];
            var rate = 1;//rates.Value<decimal>(currency);

            return amount * rate;
        }
    }
