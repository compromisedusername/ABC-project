using ABC.DTOs;
using ABC.Models;

namespace ABC.Services;

public interface IClientsService
{
    Task AddClientAsync(RequestClientAddDto client);
    Task UpdateClientAsync(int id, RequestClientUpdateDto request);
    Task DeleteClientAsync(int id);
}