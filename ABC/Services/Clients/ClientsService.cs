using ABC.DTOs;
using ABC.Exceptions;
using ABC.Models;
using ABC.Repositories.Addresses;
using ABC.Repositories.Clients;

namespace ABC.Services.Clients;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IAddressesRepository _addressesRepository;

    public ClientsService(IClientsRepository clientsRepository, IAddressesRepository addressesRepository)
    {
        _clientsRepository = clientsRepository;
        _addressesRepository = addressesRepository;
    }

    public async Task AddClientAsync(RequestClientAddDto request)
    {
        if (await _addressesRepository.GetAddressByIdAsync(request.IdAddress) == null)
        {
            throw new DomainException()
            {
                Message = "Address for given Id: " + request.IdAddress + " does not exist",
                StatusCode = 404
            };
        };
        
        Client client;
        string validatePeselOrKrs;
        if (request.ClientType == "Natural")
        {
            client = new ClientNatural
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IdAddress = request.IdAddress,
                FristName = request.FirstName,
                LastName = request.LastName,
                PESEL = request.PESEL
            };
            validatePeselOrKrs = request.PESEL;
        }
        else
        {
            client = new ClientCompany
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IdAddress = request.IdAddress,
                CompanyName = request.CompanyName,
                KRS = request.KRS
            };
            validatePeselOrKrs = request.KRS;

        }

        if (await _clientsRepository.DoesClientWithGivenPeselOrKrsExist(validatePeselOrKrs))
        {
                throw new DomainException()
                {
                    Message = "Client with given KRS or PESEL already exist!",
                    StatusCode = 400
                };
            
        }
        await _clientsRepository.AddClientAsync(client);
    }

    public async Task UpdateClientAsync(int id, RequestClientUpdateDto request)
    {
        var client = await _clientsRepository.GetClientByIdAsync(id);
        if (client != null)
        {
            client.Email = request.Email;
            client.PhoneNumber = request.PhoneNumber;
            client.IdAddress = request.IdAddress;

            if (client is ClientNatural)
            {
                var naturalClient = client as ClientNatural;
                naturalClient.FristName = request.FirstName;
                naturalClient.LastName = request.LastName;
            }
            else if (client is ClientCompany)
            {
                var companyClient = client as ClientCompany;
                companyClient.CompanyName = request.CompanyName;
            }
            await _clientsRepository.UpdateClientAsync(client);
        }
        {
            throw new DomainException()
            {
                Message = "Client not found!",
                StatusCode = 404
            };
        };
    }

    public async Task DeleteClientAsync(int id)
    {
        var res = await _clientsRepository.DeleteClientAsync(id);
        if (!res)
        {
            throw new DomainException()
            {
                Message = "Client not found!",
                StatusCode = 404
            };
        }
        
    }
}