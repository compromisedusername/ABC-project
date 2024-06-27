using ABC.DTOs;
using ABC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABC.Controllers
{
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
        public async Task<IActionResult> AddClient([FromBody] RequestClientAddDto request)
        {
             await _clientsService.AddClientAsync(request);
             return Created();
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] RequestClientUpdateDto request)
        {
            await _clientsService.UpdateClientAsync(id, request);
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientsService.DeleteClientAsync(id);
            return NoContent();
            
        }
    }
}