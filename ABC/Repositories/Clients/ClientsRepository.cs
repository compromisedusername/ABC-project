using ABC.Data;

namespace ABC.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly DatabaseContext _databaseContext;

    public ClientsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
}