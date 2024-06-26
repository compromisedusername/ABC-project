using ABC.DTOs;
using ABC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpPost]
    public async Task<IActionResult> AddClient(RequestClientAddDTO request)
    {
        return Created();
    }
    
}