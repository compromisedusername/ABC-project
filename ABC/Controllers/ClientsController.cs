using ABC.DTOs;
using ABC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] RequestClientAddDto request)
        {
             await _clientsService.AddClientAsync(request);
             return Created();
           
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] RequestClientUpdateDto request)
        {
            if (id != request.IdClient)
            {
                return BadRequest("Client id doesnt match in request.");
            }
            await _clientsService.UpdateClientAsync(id, request);
            return NoContent();
            
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientsService.DeleteClientAsync(id);
            return NoContent();
            
        }
    }
}