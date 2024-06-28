using ABC.Data;
using Microsoft.EntityFrameworkCore;

namespace ABC.Repositories.Payment;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly AppDatabaseContext _context;

    public PaymentsRepository(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetTotalPaymentsAsync()
    {
        return await _context.Payments.Where(e=>e.isRefunded==false).SumAsync(p => p.MoneyAmount);
    }

    public async Task<decimal> GetTotalPaymentsByProductAsync(int productId)
    {
        return await _context.Payments
            .Where(p => p.Contract.IdSoftwareSystem == productId && p.isRefunded==false)
            .SumAsync(p => p.MoneyAmount);
    }

    public async Task RefundAllPayments(int requestContractId)
    {
        var payments = await _context.Payments.Where(e => e.IdContract == requestContractId).ToListAsync();
        foreach (var p in payments)
        {
            p.isRefunded = true;
        }
        _context.Payments.UpdateRange(payments);
        await _context.SaveChangesAsync();
    }
    
    public async Task<decimal> GetPaymentsForUnsignedContractsAsync()
    {
        return await _context.Payments
            .Include(p => p.Contract)
            .Where(p => p.Contract != null && !p.Contract.IsSigned)
            .SumAsync(e=>e.MoneyAmount);
    }
    public async Task<decimal> GetPaymentsForUnsignedContractsByProductAsync(int productId)
    {
        return await _context.Payments
            .Include(p => p.Contract)
            .Where(p => p.Contract.IdSoftwareSystem == productId && !p.Contract.IsSigned )
            .SumAsync(e=>e.MoneyAmount);
    }
    
}