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
        return await _context.Payments.SumAsync(p => p.MoneyAmount);
    }

    public async Task<decimal> GetTotalPaymentsByProductAsync(int productId)
    {
        return await _context.Payments
            .Where(p => p.Contract.IdSoftwareSystem == productId)
            .SumAsync(p => p.MoneyAmount);
    }
}