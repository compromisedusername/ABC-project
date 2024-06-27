using ABC.Data;
using ABC.Models;
using ABC.Repositories.Discounts;

namespace ABC.Repositories.Addresses;

public class AddressesRepository : IAddressesRepository
{
    private readonly AppDatabaseContext _context;

    public AddressesRepository(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Address> GetAddressByIdAsync(int id)
    {
        return await _context.Addresses.FindAsync(id);
    }
}