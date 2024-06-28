namespace ABC.Services.Revenue;

public interface IRevenueService
{
    Task<decimal> GetCurrentRevenueAsync(string currency);
    Task<decimal> GetCurrentRevenueByProductAsync(int productId, string currency);
    Task<decimal> GetPredictedRevenueAsync(string currency);
    Task<decimal> GetPredictedRevenueByProductAsync(int productId, string currency);
}