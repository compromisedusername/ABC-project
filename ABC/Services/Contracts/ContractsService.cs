using ABC.DTOs;
using ABC.Exceptions;
using ABC.Models;
using ABC.Repositories.Clients;
using ABC.Repositories.Contracts;
using ABC.Repositories.Discounts;
using ABC.Repositories.Payment;

namespace ABC.Services.Contracts;
 public class ContractsService : IContractsService
    {
        private readonly IContractsRepository _contractsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly ISoftwareSystemsRepository _softwareSystemsRepository;
        private readonly IDiscountsRepository _discountsRepository;
        private readonly IPaymentsRepository _paymentsRepository;

        public ContractsService(IContractsRepository contractsRepository, IClientsRepository clientsRepository, ISoftwareSystemsRepository softwareSystemsRepository, IDiscountsRepository discountsRepository, IPaymentsRepository paymentsRepository)
        {
            _contractsRepository = contractsRepository;
            _clientsRepository = clientsRepository;
            _softwareSystemsRepository = softwareSystemsRepository;
            _discountsRepository = discountsRepository;
            _paymentsRepository = paymentsRepository;
        }

        public async Task<Contract> CreateContractAsync(RequestContractCreateDto request)
        {
            var client = await _clientsRepository.GetClientByIdAsync(request.ClientId);
            var softwareSystem = await _softwareSystemsRepository.GetSoftwareSystemByIdAsync(request.SoftwareSystemId);

            if (client == null || softwareSystem == null)
            {
                throw new DomainException()
                {
                    Message = "Client or Software System does not exist. Client: "+client + ", Software System: "+softwareSystem,
                    StatusCode = 400
                };
            }

            if (!await _contractsRepository.IsClientEligibleForNewContract(request.ClientId, request.SoftwareSystemId))
            {
                throw new DomainException()
                {
                    Message = "Client is not eligible for new contract",
                    StatusCode = 400
                };
            }

            var discount = await _discountsRepository.GetDiscountByIdAsync(request.DiscountId);
            var price = (decimal) ( softwareSystem.PriceForYear + (request.SupportUpdatePeriodInYears - 1) * 1000);
            
            if (discount != null)
            {
                price -= (price * discount.Value / 100);
            }

            if (await _contractsRepository.HasPreviousContracts(client.Id))
            {
                price *= 0.95m;
            }

            var contract = new Contract
            {
                IdClient = client.Id,
                IdSoftwareSystem = softwareSystem.Id,
                DateFrom = request.StartDate,
                DateTo = request.StartDate.AddDays(new Random().Next(3, 31)),
                Price = price,
                SupportUpdatePeriodInYears = request.SupportUpdatePeriodInYears,
                UpdateInformation = "Possible updates in future",
                IdDiscount = request.DiscountId,
                IsActive = true
            };

             await _contractsRepository.AddContractAsync(contract);
             return contract;
        }

        public async Task<Payment> CreatePaymentAsync(RequestPaymentCreateDto request)
        {
            
            if (request.ClientId != await _contractsRepository.GetClientIdFromContractIdAsync(request.ContractId))
            {
                throw new DomainException()
                {
                    Message = "Client assigned to payment is different in request.",
                    StatusCode = 400
                };
            }

            if (await _clientsRepository.GetClientByIdAsync(request.ClientId) is null)
            {
                throw new DomainException()
                {
                    Message = "Client for given Id: " + request.ClientId + " not found.",
                    StatusCode = 404
                };
            }

            
            
            var contract = await _contractsRepository.GetContractByIdAsync(request.ContractId);

            
            
            if (contract == null ) 
            {
                throw new DomainException()
                {
                    Message = "Given contract does not exist.",
                    StatusCode = 400
                };
            }

            if (DateTime.Now > contract.DateTo)
            {
                
                await _paymentsRepository.RefundAllPayments(request.ContractId);
                await _contractsRepository.DeactiaveContract(request.ContractId);
                throw new DomainException()
                {
                    Message = "Payment date is not up to date. Payment date: " + DateTime.Now + "Contract Payment Date: " + contract.DateTo,
                    StatusCode = 400
                };
            }

            var totalPaid = await _contractsRepository.GetTotalPaymentsForContract(request.ContractId);
            if (totalPaid + request.Amount > contract.Price)
            {
                var tooMuch = totalPaid + request.Amount - contract.Price ;
                throw new DomainException()
                {
                    Message = "Amount of paid is to much by: "+ tooMuch+ " pln. Total paid: " + totalPaid + ", Payment paid: " + request.Amount + ", Contract price: " + contract.Price,
                    StatusCode = 400
                };
            }

            var payment = new Payment
            {
                IdContract = contract.Id,
                MoneyAmount = request.Amount,
                Date = DateTime.Now,
                IdClient = contract.IdClient
            };

            var result = await _contractsRepository.AddPaymentAsync(payment);
            
            if (result)
            {
                totalPaid += request.Amount;
                if (totalPaid >= contract.Price)
                {
                    contract.IsSigned = true;
                    contract.IsActive = false;
                    await _contractsRepository.UpdateContractAsync(contract);
                }
            }

            return payment;
        }
    }