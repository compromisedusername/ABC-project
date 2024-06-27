using ABC.Data;
using ABC.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC.Repositories.Clients
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly AppDatabaseContext _context;

        public ClientsRepository(AppDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            _context.Clients.Add(client);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<bool> DoesClientWithGivenPeselOrKrsExist(string validatePeselOrKrs)
        {
            return await _context.ClientsCompanies.FirstOrDefaultAsync(e => e.KRS == validatePeselOrKrs) != null ||
                   await _context.ClientsNaturals.FirstOrDefaultAsync(e => e.PESEL == validatePeselOrKrs) != null;
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                if (client is ClientNatural)
                {
                    var naturalClient = client as ClientNatural;
                    naturalClient.IsDeleted = true;
                }
                else if (client is ClientCompany)
                {
                    return false;
                }

                _context.Clients.Update(client);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}