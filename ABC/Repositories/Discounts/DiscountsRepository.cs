using ABC.Data;
using ABC.Models;

namespace ABC.Repositories.Discounts;

public class DiscountsRepository : IDiscountsRepository
{
    private readonly AppDatabaseContext _context;

    public DiscountsRepository(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Discount> GetDiscountByIdAsync(int id)
    {
        return await _context.Discounts.FindAsync(id);
    }
}