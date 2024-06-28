using ABC.DTOs.ExternalAPIs.ExchangeRate;

namespace ABC.Services.ExchangeRates;

public interface IExchangeRateService
{
     Task<ExchangeRateResponse> GetExchangeRatesAsync( string baseCurrency);

}