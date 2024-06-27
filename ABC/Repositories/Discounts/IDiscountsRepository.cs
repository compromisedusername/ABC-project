using ABC.Models;

namespace ABC.Repositories.Discounts;

public interface IDiscountsRepository
{
    Task<Discount> GetDiscountByIdAsync(int id);
}