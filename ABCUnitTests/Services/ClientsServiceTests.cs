using System.Threading.Tasks;
using ABC.DTOs;
using ABC.Exceptions;
using ABC.Models;
using ABC.Repositories.Addresses;
using ABC.Repositories.Clients;
using ABC.Services.Clients;
using Moq;
using Xunit;

public class ClientsServiceTests
{
    private readonly Mock<IClientsRepository> _mockClientsRepository;
    private readonly Mock<IAddressesRepository> _mockAddressesRepository;
    private readonly ClientsService _service;

    public ClientsServiceTests()
    {
        _mockClientsRepository = new Mock<IClientsRepository>();
        _mockAddressesRepository = new Mock<IAddressesRepository>();
        _service = new ClientsService(_mockClientsRepository.Object, _mockAddressesRepository.Object);
    }

    [Fact]
    public async Task AddClientAsync_ShouldThrowException_WhenAddressDoesNotExist()
    {
        // Arrange
        var request = new RequestClientAddDto { IdAddress = 1, ClientType = "Natural", PESEL = "12345678901" };
        _mockAddressesRepository.Setup(repo => repo.GetAddressByIdAsync(request.IdAddress)).ReturnsAsync((Address)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.AddClientAsync(request));
        Assert.Equal("Address for given Id: 1 does not exist", exception.Message);
    }

    [Fact]
    public async Task AddClientAsync_ShouldThrowException_WhenClientWithGivenPeselOrKrsExist()
    {
        // Arrange
        var request = new RequestClientAddDto { IdAddress = 1, ClientType = "Natural", PESEL = "12345678901" };
        _mockAddressesRepository.Setup(repo => repo.GetAddressByIdAsync(request.IdAddress)).ReturnsAsync(new Address());
        _mockClientsRepository.Setup(repo => repo.DoesClientWithGivenPeselOrKrsExist(request.PESEL)).ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.AddClientAsync(request));
        Assert.Equal("Client with given KRS or PESEL already exist!", exception.Message);
    }

    [Fact]
    public async Task AddClientAsync_ShouldAddClient_WhenDataIsValid()
    {
        // Arrange
        var request = new RequestClientAddDto { IdAddress = 1, ClientType = "Natural", PESEL = "12345678901", Email = "test@test.com", PhoneNumber = "123456789", FirstName = "John", LastName = "Doe" };
        _mockAddressesRepository.Setup(repo => repo.GetAddressByIdAsync(request.IdAddress)).ReturnsAsync(new Address());
        _mockClientsRepository.Setup(repo => repo.DoesClientWithGivenPeselOrKrsExist(request.PESEL)).ReturnsAsync(false);

        // Act
        await _service.AddClientAsync(request);

        // Assert
        _mockClientsRepository.Verify(repo => repo.AddClientAsync(It.IsAny<Client>()));
    }

    [Fact]
    public async Task UpdateClientAsync_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        var request = new RequestClientUpdateDto { Email = "test@test.com", PhoneNumber = "123456789", IdAddress = 1 };
        _mockClientsRepository.Setup(repo => repo.GetClientByIdAsync(It.IsAny<int>())).ReturnsAsync((Client)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.UpdateClientAsync(1, request));
        Assert.Equal("Client not found!", exception.Message);
    }


    [Fact]
    public async Task DeleteClientAsync_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        _mockClientsRepository.Setup(repo => repo.DeleteClientAsync(It.IsAny<int>())).ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => _service.DeleteClientAsync(1));
        Assert.Equal("Client not found!", exception.Message);
    }

    [Fact]
    public async Task DeleteClientAsync_ShouldDeleteClient_WhenClientExists()
    {
        // Arrange
        _mockClientsRepository.Setup(repo => repo.DeleteClientAsync(It.IsAny<int>())).ReturnsAsync(true);

        // Act
        await _service.DeleteClientAsync(1);

        // Assert
        _mockClientsRepository.Verify(repo => repo.DeleteClientAsync(It.IsAny<int>()), Times.Once);
    }
}
