using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using ABC.Repositories.Contracts;
using ABC.Repositories.Payment;
using ABC.Services.ExchangeRates;

namespace ABC.Services.Revenue;

public class RevenueService : IRevenueService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IContractsRepository _contractsRepository;
        private readonly IConfiguration _config;


        public RevenueService(IPaymentsRepository paymentsRepository, IContractsRepository contractsRepository, IConfiguration config)
        {
            _paymentsRepository = paymentsRepository;
            _contractsRepository = contractsRepository;
            _config = config;
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
            var exchangeRateSerivce = new ExchangeRateService(client, _config);
            var exchangeRateResponse = await exchangeRateSerivce.GetExchangeRatesAsync("PLN");
            var rate = exchangeRateResponse.ConversionRates[currency];
            return amount * rate;
        }
    }
