using ABC.Controllers;
using ABC.DTOs;
using ABC.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ABCUnitTests;

public class ClientsControllerTests
    {
        private readonly ClientsController _controller;
        private readonly Mock<IClientsService> _mockService;

        public ClientsControllerTests()
        {
            _mockService = new Mock<IClientsService>();
            _controller = new ClientsController(_mockService.Object);
        }

        [Fact]
        public async Task AddClient_ReturnsCreatedResult_WhenClientIsAdded()
        {
            // Arrange
            var newClient = new RequestClientAddDto
            {
                
            };

            _mockService.Setup(service => service.AddClientAsync(It.IsAny<RequestClientAddDto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddClient(newClient);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task UpdateClient_ReturnsNoContent_WhenClientIsUpdated()
        {
            // Arrange
            var clientId = 1;
            var updateRequest = new RequestClientUpdateDto
            {
                IdClient = clientId,
            };

            _mockService.Setup(service => service.UpdateClientAsync(clientId, It.IsAny<RequestClientUpdateDto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateClient(clientId, updateRequest);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateClient_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var clientId = 1;
            var updateRequest = new RequestClientUpdateDto
            {
                IdClient = 2,
            };

            // Act
            var result = await _controller.UpdateClient(clientId, updateRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Client id doesnt match in request.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNoContent_WhenClientIsDeleted()
        {
            // Arrange
            var clientId = 1;

            _mockService.Setup(service => service.DeleteClientAsync(clientId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteClient(clientId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }