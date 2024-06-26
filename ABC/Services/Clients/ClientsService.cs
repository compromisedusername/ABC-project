namespace ABC.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsService _clientsService;

    public ClientsService(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
}