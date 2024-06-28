using ABC.Data;
using ABC.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC.Repositories.Contracts;


public class ContractsRepository : IContractsRepository
{
    private readonly AppDatabaseContext _context;

    public ContractsRepository(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> AddContractAsync(Contract contract)
    {
        _context.Contracts.Add(contract);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Contract> GetContractByIdAsync(int id)
    {
        return await _context.Contracts.FindAsync(id);
    }

    public async Task<bool> UpdateContractAsync(Contract contract)
    {
        _context.Contracts.Update(contract);
        return await _context.SaveChangesAsync() > 0;
    }
    

    public async Task<bool> AddPaymentAsync(Models.Payment payment)
    {
        _context.Payments.Add(payment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> IsClientEligibleForNewContract(int clientId, int softwareSystemId)
    {
        return !await _context.Contracts.AnyAsync(c => c.IdClient == clientId && c.IdSoftwareSystem == softwareSystemId && c.IsActive);
    }

    public async Task<bool> HasPreviousContracts(int clientId)
    {
        return await _context.Contracts.AnyAsync(c => c.IdClient == clientId && (c.IsSigned || c.IsActive));
    }

    public async Task<decimal> GetTotalPaymentsForContract(int contractId)
    {
        return await _context.Payments.Where(p => p.IdContract == contractId ).SumAsync(p => p.MoneyAmount);
    }

    public async Task<decimal> GetPredictedRevenueAsync()
    {
        return await _context.Contracts
            .Where(c => !c.IsSigned || c.IsActive)
            .SumAsync(c => c.Price);
    }

    public async Task<decimal> GetPredictedRevenueByProductAsync(int productId)
    {
        return await _context.Contracts
            .Where(c => (c.IdSoftwareSystem == productId) && (!c.IsSigned || c.IsActive))
            .SumAsync(c => c.Price);
    }
    public async Task<int> GetClientIdFromContractIdAsync(int requestContractId)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(e => e.Id == requestContractId);
        return contract.IdClient;
    }

    public async Task DeactiaveContract(int requestContractId)
    {
        var contract = await _context.Contracts.FindAsync(requestContractId);
        contract.IsActive = false;
        contract.IsSigned = false;
        _context.Contracts.Update(contract);
        await _context.SaveChangesAsync();
    }

    
}
