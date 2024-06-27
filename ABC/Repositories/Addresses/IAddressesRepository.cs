using ABC.Models;

namespace ABC.Repositories.Addresses;

public interface IAddressesRepository
{
    Task<Address> GetAddressByIdAsync(int id);

}