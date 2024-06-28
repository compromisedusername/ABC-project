using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using ABC.Exceptions;
using ABC.Repositories.Contracts;
using ABC.Repositories.Payment;
using ABC.Services.ExchangeRates;

namespace ABC.Services.Revenue;

public class RevenueService : IRevenueService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IContractsRepository _contractsRepository;
        private readonly IExchangeRateService _exchangeRateService;

        public RevenueService(IPaymentsRepository paymentsRepository, IContractsRepository contractsRepository, IConfiguration config, IExchangeRateService exchangeRateService)
        {
            _paymentsRepository = paymentsRepository;
            _contractsRepository = contractsRepository;
            _exchangeRateService = exchangeRateService;
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

            /*try
            {*/
                var exchangeRateResponse =  await _exchangeRateService.GetExchangeRatesAsync("PLN");
                var rate = exchangeRateResponse.ConversionRates[currency.ToLower()];
                return amount * rate;

            /*}
            catch (Exception e)
            {
                throw new DomainException()
                {
                    Message = "Given currency does not exist. " + e.Message,
                    StatusCode = 404
                };
            }*/
        
        }
    }
