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

    public async Task<bool> AddPaymentAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> IsClientEligibleForNewContract(int clientId, int softwareSystemId)
    {
        return !await _context.Contracts.AnyAsync(c => c.IdClient == clientId && c.IdSoftwareSystem == softwareSystemId && c.IsActive);
    }

    public async Task<decimal> GetTotalPaymentsForContract(int contractId)
    {
        return await _context.Payments.Where(p => p.IdContract == contractId).SumAsync(p => p.MoneyAmount);
    }

    public async Task<bool> HasPreviousContracts(int clientId)
    {
        return await _context.Contracts.AnyAsync(c => c.IdClient == clientId && (c.IsSigned || c.IsActive));
    }
}
