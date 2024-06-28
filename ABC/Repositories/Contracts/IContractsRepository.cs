using ABC.Models;

namespace ABC.Repositories.Contracts;

public interface IContractsRepository
{
    Task<bool> AddContractAsync(Contract contract);
    Task<Contract> GetContractByIdAsync(int id);
    Task<bool> UpdateContractAsync(Contract contract);
    Task<bool> AddPaymentAsync(Models.Payment payment);
    Task<bool> IsClientEligibleForNewContract(int clientId, int softwareSystemId);
    Task<decimal> GetTotalPaymentsForContract(int contractId);
    Task<bool> HasPreviousContracts(int clientId);
    
    Task<decimal> GetPredictedRevenueAsync();
    Task<decimal> GetPredictedRevenueByProductAsync(int productId);
    Task<int> GetClientIdFromContractIdAsync(int requestContractId);

    Task DeactiaveContract(int requestContractId);

}