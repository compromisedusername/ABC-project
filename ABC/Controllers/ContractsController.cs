using ABC.DTOs;
using ABC.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABC.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ContractsController : ControllerBase
{
    private readonly IContractsService _contractsService;

    public ContractsController(IContractsService contractsService)
    {
        _contractsService = contractsService;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] RequestContractCreateDto request)
    {
        var result = await _contractsService.CreateContractAsync(request);
        return Created("api/Contracts/created",result);
    }
    [Authorize]
    [HttpPost("pay")]
    public async Task<IActionResult> CreatePayment([FromBody] RequestPaymentCreateDto request)
    {
        var result = await _contractsService.CreatePaymentAsync(request);
        
            return Created("api/Contracts/pay/created/",result);
        
    }
}