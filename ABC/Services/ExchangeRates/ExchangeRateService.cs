using System.Text.Json;
using ABC.DTOs.ExternalAPIs.ExchangeRate;

namespace ABC.Services.ExchangeRates;

public class ExchangeRateService : IExchangeRateService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;


    public ExchangeRateService(HttpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
    }

    
    public async Task<ExchangeRateResponse> GetExchangeRatesAsync(string baseCurrency)
    {
        var apiKey = _config["Rates:ServiceApiKey"];
        var response = await _client.GetStringAsync($"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}");
        var exchangeRateResponse = JsonSerializer.Deserialize<ExchangeRateResponse>(response);
        return exchangeRateResponse;
    }

    
}