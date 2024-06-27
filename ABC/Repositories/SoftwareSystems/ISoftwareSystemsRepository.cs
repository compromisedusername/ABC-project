using ABC.Models;

namespace ABC.Repositories.Discounts;

public interface ISoftwareSystemsRepository
{
    Task<SoftwareSystem> GetSoftwareSystemByIdAsync(int id);
}