using System.Threading.Tasks;
using ABC.DTOs;
using ABC.Exceptions;
using ABC.Models;
using ABC.Repositories.Clients;
using ABC.Repositories.Contracts;
using ABC.Repositories.Discounts;
using ABC.Repositories.Payment;
using ABC.Repositories.SoftwareSystems;
using ABC.Services.Contracts;
using Moq;
using Xunit;

public class ContractsServiceTests
{
    private readonly Mock<IContractsRepository> _mockContractsRepository;
    private readonly Mock<IClientsRepository> _mockClientsRepository;
    private readonly Mock<ISoftwareSystemsRepository> _mockSoftwareSystemsRepository;
    private readonly Mock<IDiscountsRepository> _mockDiscountsRepository;
    private readonly Mock<IPaymentsRepository> _mockPaymentsRepository;
    private readonly ContractsService _service;

    public ContractsServiceTests()
    {
        _mockContractsRepository = new Mock<IContractsRepository>();
        _mockClientsRepository = new Mock<IClientsRepository>();
        _mockSoftwareSystemsRepository = new Mock<ISoftwareSystemsRepository>();
        _mockDiscountsRepository = new Mock<IDiscountsRepository>();
        _mockPaymentsRepository = new Mock<IPaymentsRepository>();
        _service = new ContractsService(_mockContractsRepository.Object, _mockClientsRepository.Object, _mockSoftwareSystemsRepository.Object, _mockDiscountsRepository.Object, _mockPaymentsRepository.Object);
    }

    

    [Fact]
    public async Task CreateContractAsync_ShouldThrowException_WhenClientIsNotEligibleForNewContract()
    {
        // Arrange
        var request = new RequestContractCreateDto { ClientId = 1, SoftwareSystemId = 1, StartDate = DateTime.Now, SupportUpdatePeriodInYears = 1 };
        _mockClientsRepository.Setup(repo => repo.GetClientByIdAsync(request.ClientId)).ReturnsAsync(new ClientNatural());
        _mockSoftwareSystemsRepository.Setup(repo => repo.GetSoftwareSystemByIdAsync(request.SoftwareSystemId)).ReturnsAsync(new SoftwareSystem());
        _mockContractsRepository.Setup(repo => repo.IsClientEligibleForNewContract(request.ClientId, request.SoftwareSystemId)).ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.CreateContractAsync(request));
        Assert.Equal("Client is not eligible for new contract", exception.Message);
    }
    

    [Fact]
    public async Task CreatePaymentAsync_ShouldThrowException_WhenClientIdMismatch()
    {
        // Arrange
        var request = new RequestPaymentCreateDto { ClientId = 1, ContractId = 1, Amount = 100, PaymentDate = DateTime.Now };
        _mockContractsRepository.Setup(repo => repo.GetClientIdFromContractIdAsync(request.ContractId)).ReturnsAsync(2);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.CreatePaymentAsync(request));
        Assert.Equal("Client assigned to payment is different in request.", exception.Message);
    }

    [Fact]
    public async Task CreatePaymentAsync_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        var request = new RequestPaymentCreateDto { ClientId = 1, ContractId = 1, Amount = 100, PaymentDate = DateTime.Now };
        _mockContractsRepository.Setup(repo => repo.GetClientIdFromContractIdAsync(request.ContractId)).ReturnsAsync(request.ClientId);
        _mockClientsRepository.Setup(repo => repo.GetClientByIdAsync(request.ClientId)).ReturnsAsync((Client)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.CreatePaymentAsync(request));
        Assert.Equal("Client for given Id: 1 not found.", exception.Message);
    }

    [Fact]
    public async Task CreatePaymentAsync_ShouldThrowException_WhenContractDoesNotExist()
    {
        // Arrange
        var request = new RequestPaymentCreateDto { ClientId = 1, ContractId = 1, Amount = 100, PaymentDate = DateTime.Now };
        _mockContractsRepository.Setup(repo => repo.GetClientIdFromContractIdAsync(request.ContractId)).ReturnsAsync(request.ClientId);
        _mockClientsRepository.Setup(repo => repo.GetClientByIdAsync(request.ClientId)).ReturnsAsync(new ClientNatural());
        _mockContractsRepository.Setup(repo => repo.GetContractByIdAsync(request.ContractId)).ReturnsAsync((Contract)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.CreatePaymentAsync(request));
        Assert.Equal("Given contract does not exist.", exception.Message);
    }

 
}
