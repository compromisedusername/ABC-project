namespace ABC.Repositories.Payment;

public interface IPaymentsRepository
{
    Task<decimal> GetTotalPaymentsAsync();
    Task<decimal> GetTotalPaymentsByProductAsync(int productId);
    Task RefundAllPayments(int requestContractId);
    Task<decimal> GetPaymentsForUnsignedContractsAsync();
    Task<decimal> GetPaymentsForUnsignedContractsByProductAsync(int productId);


}