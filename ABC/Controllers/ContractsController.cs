using ABC.DTOs;
using ABC.Models;
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
        var contract = await _contractsService.CreateContractAsync(request);
        var result = new
        {
            UpdateInformation = contract.UpdateInformation,
            IdClient = contract.IdClient,
            Price = contract.Price
        };
        return Created("api/Contracts/created",result);
    }
    [Authorize]
    [HttpPost("pay")]
    public async Task<IActionResult> CreatePayment([FromBody] RequestPaymentCreateDto request)
    {
        var payment = await _contractsService.CreatePaymentAsync(request);
        var result = new
        {
            AmountPaid = payment.MoneyAmount,
            IdClient = payment.IdClient,
            IdContract = payment.IdContract
        };
        return Created("api/Contracts/pay/created/", result
            );
        
    }
}