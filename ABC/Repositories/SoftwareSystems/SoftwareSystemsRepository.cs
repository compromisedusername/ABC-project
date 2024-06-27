using ABC.Data;
using ABC.Models;
using ABC.Repositories.Discounts;

namespace ABC.Repositories.SoftwareSystems;

public class SoftwareSystemsRepository : ISoftwareSystemsRepository
{
    private readonly AppDatabaseContext _context;

    public SoftwareSystemsRepository(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<SoftwareSystem> GetSoftwareSystemByIdAsync(int id)
    {
        return await _context.SoftwareSystems.FindAsync(id);
    }
}