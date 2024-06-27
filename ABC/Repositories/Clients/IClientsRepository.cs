using ABC.Models;

namespace ABC.Repositories.Clients;

public interface IClientsRepository
{
    Task<bool> AddClientAsync(Client client);
    Task<bool> UpdateClientAsync(Client client);
    Task<bool> DeleteClientAsync(int id);
    Task<Client> GetClientByIdAsync(int id);

    Task<bool> DoesClientWithGivenPeselOrKrsExist(string validatePeselOrKrs);
}