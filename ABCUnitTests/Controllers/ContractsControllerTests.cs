using ABC.Controllers;
using ABC.DTOs;
using ABC.Models;
using ABC.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ABCUnitTests;

public class ContractsControllerTests
{
    private readonly ContractsController _controller;
    private readonly Mock<IContractsService> _mockService;

    public ContractsControllerTests()
    {
        _mockService = new Mock<IContractsService>();
        _controller = new ContractsController(_mockService.Object);
    }

    [Fact]
    public async Task CreateContract_ReturnsCreatedResult_WhenContractIsCreated()
    {
        // Arrange
        var newContract = new RequestContractCreateDto
        {
            
        };

        var createdContract = new Contract
        {
            
        };

        _mockService.Setup(service => service.CreateContractAsync(It.IsAny<RequestContractCreateDto>()))
            .ReturnsAsync(createdContract);

        // Act
        var result = await _controller.CreateContract(newContract);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("api/Contracts/created", createdResult.Location);
    }

    [Fact]
    public async Task CreatePayment_ReturnsCreatedResult_WhenPaymentIsCreated()
    {
        // Arrange
        var newPayment = new RequestPaymentCreateDto
        {
            
        };

        var createdPayment = new Payment
        {
          
        };

        _mockService.Setup(service => service.CreatePaymentAsync(It.IsAny<RequestPaymentCreateDto>()))
            .ReturnsAsync(createdPayment);

        // Act
        var result = await _controller.CreatePayment(newPayment);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("api/Contracts/pay/created/", createdResult.Location);
    }
}