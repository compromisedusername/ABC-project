namespace ABC.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsService _clientsService;

    public ClientsService(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    
    /*todo
     
     numer PESEL, numer KRS nie może być zmieniany po jego wprowadzeniu.
        
    Usuwając dane o kliencie indywidualnym, nadpisujemy dane w bazie, ale zachowujemy sam rekord w bazie danych. 
        
    Dane o firmach nie mogą być usuwane.*/
}