using ABC.DTOs;
using ABC.Models;

namespace ABC.Services.Contracts;

public interface IContractsService
{
    Task<Contract> CreateContractAsync(RequestContractCreateDto request);
    Task<Payment> CreatePaymentAsync(RequestPaymentCreateDto request);
}