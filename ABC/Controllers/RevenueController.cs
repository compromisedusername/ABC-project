using ABC.Services.Revenue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentRevenue([FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.GetCurrentRevenueAsync(currency);
        return Ok(revenue);
    }
    [Authorize]
    [HttpGet("current/{productId}")]
    public async Task<IActionResult> GetCurrentRevenueByProduct(int productId, [FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.GetCurrentRevenueByProductAsync(productId, currency);
        return Ok(revenue);
    }
    
    [Authorize]
    [HttpGet("predicted")]
    public async Task<IActionResult> GetPredictedRevenue([FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.GetPredictedRevenueAsync(currency);
        return Ok(revenue);
    }
    
    [Authorize]
    [HttpGet("predicted/{productId}")]
    public async Task<IActionResult> GetPredictedRevenueByProduct(int productId, [FromQuery] string currency = "PLN")
    {
        var revenue = await _revenueService.GetPredictedRevenueByProductAsync(productId, currency);
        return Ok(revenue);
    }
}

