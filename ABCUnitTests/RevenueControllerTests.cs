using ABC.Controllers;
using ABC.DTOs.Responses;
using ABC.Services.Revenue;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ABCUnitTests;


    public class RevenueControllerTests
    {
        private readonly RevenueController _controller;
        private readonly Mock<IRevenueService> _mockService;

        public RevenueControllerTests()
        {
            _mockService = new Mock<IRevenueService>();
            _controller = new RevenueController(_mockService.Object);
        }

        [Fact]
        public async Task GetCurrentRevenue_ReturnsOkResult_WithRevenueData()
        {
            // Arrange
            var expectedRevenue = 1000.0m;
            _mockService.Setup(service => service.GetCurrentRevenueAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRevenue);

            // Act
            var result = await _controller.GetCurrentRevenue("USD");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseRevenueDto>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(expectedRevenue, response.Value);
        }

        [Fact]
        public async Task GetCurrentRevenueByProduct_ReturnsOkResult_WithRevenueData()
        {
            // Arrange
            var productId = 1;
            var expectedRevenue = 500.0m;
            _mockService.Setup(service => service.GetCurrentRevenueByProductAsync(productId, It.IsAny<string>()))
                .ReturnsAsync(expectedRevenue);

            // Act
            var result = await _controller.GetCurrentRevenueByProduct(productId, "USD");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseRevenueDto>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(expectedRevenue, response.Value);
        }

        [Fact]
        public async Task GetPredictedRevenue_ReturnsOkResult_WithRevenueData()
        {
            // Arrange
            var expectedRevenue = 1200.0m;
            _mockService.Setup(service => service.GetPredictedRevenueAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRevenue);

            // Act
            var result = await _controller.GetPredictedRevenue("USD");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseRevenueDto>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(expectedRevenue, response.Value);
        }

        [Fact]
        public async Task GetPredictedRevenueByProduct_ReturnsOkResult_WithRevenueData()
        {
            // Arrange
            var productId = 1;
            var expectedRevenue = 600.0m;
            _mockService.Setup(service => service.GetPredictedRevenueByProductAsync(productId, It.IsAny<string>()))
                .ReturnsAsync(expectedRevenue);

            // Act
            var result = await _controller.GetPredictedRevenueByProduct(productId, "USD");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseRevenueDto>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(expectedRevenue, response.Value);
        }
    }
