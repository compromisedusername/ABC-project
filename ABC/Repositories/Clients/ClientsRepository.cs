using ABC.Data;

namespace ABC.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public ClientsRepository(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }
}